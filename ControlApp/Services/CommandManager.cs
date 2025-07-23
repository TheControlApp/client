// ==== File: ControlApp\Services\CommandManager.cs ====

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using ControlApp.Commands;
using ControlApp.Utils;

namespace ControlApp.Services
{
    /// <summary>
    /// Represents a complete command payload received from a single sender.
    /// This is the atomic unit of work for the command queue.
    /// </summary>
    public record CommandPayloadTask(CommandPayload Payload);

    /// <summary>
    /// Manages the queue of incoming command payloads, tracks execution state, and provides a centralized point of control.
    /// This is a thread-safe, static class.
    /// </summary>
    public static class CommandManager
    {
        private static readonly Queue<CommandPayloadTask> _payloadQueue = new Queue<CommandPayloadTask>();
        private static readonly object _lock = new object(); // Used for ensuring thread safety

        /// <summary>
        /// Gets the most recently executed command payload task. Can be null if no payload has run yet.
        /// </summary>
        public static CommandPayloadTask? LastExecutedPayload { get; private set; }

        /// <summary>
        /// Gets the number of command payloads currently pending in the queue.
        /// </summary>
        public static int PendingPayloadCount
        {
            get
            {
                lock (_lock)
                {
                    return _payloadQueue.Count;
                }
            }
        }

        /// <summary>
        /// Fires whenever the payload queue is modified (payloads added or removed) or cleared.
        /// The UI should subscribe to this to update its state.
        /// </summary>
        public static event Action OnQueueChanged;

        /// <summary>
        /// Adds a complete CommandPayload to the execution queue as a single task.
        /// </summary>
        /// <param name="payload">The CommandPayload received from the server.</param>
        /// <param name="senderId">The ID of the user who sent the commands.</param>
        public static void EnqueuePayload(CommandPayload payload)
        {
            if (payload?.Commands == null || !payload.Commands.Any())
            {
                return;
            }

            var payloadTask = new CommandPayloadTask(payload);

            lock (_lock)
            {
                _payloadQueue.Enqueue(payloadTask);
                Utilities.LogInfo($"Enqueued payload with {payload.Commands.Count} commands from '{payload.SenderUsername}'.");
            }

            // Notify subscribers that the queue has been updated.
            OnQueueChanged?.Invoke();
        }

        /// <summary>
        /// Retrieves the next command payload from the queue without executing or removing it.
        /// </summary>
        /// <returns>The next CommandPayloadTask to be executed, or null if the queue is empty.</returns>
        public static CommandPayloadTask? PeekNextPayload()
        {
            lock (_lock)
            {
                if (_payloadQueue.TryPeek(out var nextTask))
                {
                    return nextTask;
                }
            }
            return null;
        }

        /// <summary>
        /// Dequeues and executes all commands within the next payload in the queue.
        /// Sets the executed payload as the LastExecutedPayload.
        /// </summary>
        public static async Task ExecuteNextPayloadAsync()
        {
            CommandPayloadTask? taskToRun = null;
            lock (_lock)
            {
                if (_payloadQueue.TryDequeue(out taskToRun))
                {
                    LastExecutedPayload = taskToRun;
                }
            }

            if (taskToRun != null)
            {
                ExecutePayload(taskToRun);
                // Notify the server that this specific payload was executed.
                await WebSocketsCommunicator.AcknowledgePayloadExecutionAsync(taskToRun);
                // Notify that the queue has changed (one item was removed).
                OnQueueChanged?.Invoke();
            }
            else
            {
                Utilities.LogInfo("ExecuteNextPayload called, but the queue is empty.");
            }
        }

        /// <summary>
        /// Re-executes all commands from the most recently run payload.
        /// Does not affect the command queue.
        /// </summary>
        public static async Task ExecuteLastPayloadAsync()
        {
            var lastPayload = LastExecutedPayload; // Local copy for thread safety

            if (lastPayload != null)
            {
                ExecutePayload(lastPayload, isRerun: true);
                // Notify the server that this specific payload was re-executed.
                await WebSocketsCommunicator.AcknowledgePayloadExecutionAsync(lastPayload);
            }
            else
            {
                Utilities.LogInfo("ExecuteLastPayload called, but no payload has been executed yet.");
            }
        }

        /// <summary>
        /// Core execution logic for a CommandPayloadTask.
        /// </summary>
        private static void ExecutePayload(CommandPayloadTask payloadTask, bool isRerun = false)
        {
            string action = isRerun ? "Re-executing" : "Executing";
            Utilities.LogInfo($"{action} payload with {payloadTask.Payload.Commands.Count} commands from '{payloadTask.Payload.SenderUsername}'.");

            payloadTask.Payload.Execute();
        }

        /// <summary>
        /// Dequeues and executes all pending payloads in the queue sequentially.
        /// This method will run until the queue is empty.
        /// </summary>
        public static async Task ExecuteAllPayloadsAsync()
        {
            Utilities.LogInfo("Starting execution of all pending payloads.");
            while (PendingPayloadCount > 0)
            {
                // We await each execution to ensure they happen sequentially and
                // the acknowledgements are sent in order.
                await ExecuteNextPayloadAsync();
            }
            Utilities.LogInfo("Finished executing all payloads.");
        }

        /// <summary>
        /// Blocks the sender of the most recently executed command payload.
        /// It sends a request to the server to perform the block action.
        /// </summary>
        /// <returns>A tuple containing a boolean indicating success and a string with a user-facing message.</returns>
        public static async Task<(bool Success, string Message)> BlockLastCommandSender()
        {
            var lastPayload = LastExecutedPayload; // Thread-safe copy

            if (lastPayload == null)
            {
                return (false, "No command has been executed yet, so there is no sender to block.");
            }

            // It's often useful to prevent users from blocking anonymous or system-level senders.
            if (lastPayload.Payload.SenderUsername == "-1" || string.IsNullOrEmpty(lastPayload.Payload.SenderUsername))
            {
                return (false, "Cannot block an anonymous or system sender.");
            }

            bool success = await ServerCommunicator.BlockSenderAsync(lastPayload.Payload.SenderUsername);

            if (success)
            {
                return (true, $"User '{lastPayload.Payload.SenderUsername}' has been successfully blocked.");
            }
            else
            {
                return (false, $"Failed to block user '{lastPayload.Payload.SenderUsername}'. Please check the logs for more details.");
            }
        }

        /// <summary>
        /// Reports the sender of the most recently executed command payload.
        /// It serializes the last command payload and sends it to the server for context.
        /// </summary>
        /// <returns>A tuple containing a boolean indicating success and a string with a user-facing message.</returns>
        public static async Task<(bool Success, string Message)> ReportLastCommandSenderAsync()
        {
            var lastPayloadTask = LastExecutedPayload; // Thread-safe copy

            if (lastPayloadTask == null)
            {
                return (false, "No command has been executed yet, so there is no one to report.");
            }

            // Serialize the payload that is being reported to provide context to the server.
            string payloadJson = JsonSerializer.Serialize(lastPayloadTask.Payload);

            bool success = await ServerCommunicator.ReportSenderAsync(lastPayloadTask.Payload.SenderUsername, payloadJson);

            if (success)
            {
                return (true, $"User '{lastPayloadTask.Payload.SenderUsername}' has been successfully reported.");
            }
            else
            {
                return (false, $"Failed to report user '{lastPayloadTask.Payload.SenderUsername}'. Please check the logs for more details.");
            }
        }

        /// <summary>
        /// Clears all pending payloads from the queue.
        /// </summary>
        public static async Task ClearQueueAndNotifyServerAsync()
        {
            List<int> clearedPayloadIds = new List<int>();
            lock (_lock)
            {
                // Collect the IDs of all payloads that are about to be cleared.
                foreach (var task in _payloadQueue)
                {
                    clearedPayloadIds.Add(task.Payload.CommandId);
                }
                _payloadQueue.Clear();
                Utilities.LogInfo("Command payload queue has been cleared locally.");
            }

            if (clearedPayloadIds.Any())
            {
                // Notify the server about the cleared payloads.
                await WebSocketsCommunicator.AcknowledgeQueueClearedAsync(clearedPayloadIds);
            }

            OnQueueChanged?.Invoke();
        }

        /// <summary>
        /// Sends a "like" or "thumbs up" for the sender of the most recently executed command payload.
        /// </summary>
        /// <returns>A tuple containing a boolean indicating success and a string with a user-facing message.</returns>
        public static async Task<(bool Success, string Message)> LikeLastCommandSenderAsync()
        {
            var lastPayload = LastExecutedPayload; // Thread-safe copy

            if (lastPayload == null)
            {
                return (false, "No command has been executed yet, so there is no sender to like.");
            }

            // Prevent liking anonymous or system-level senders.
            if (lastPayload.Payload.SenderUsername == "-1" || string.IsNullOrEmpty(lastPayload.Payload.SenderUsername))
            {
                return (false, "Cannot give a thumbs up to an anonymous sender.");
            }

            bool success = await ServerCommunicator.LikeSenderAsync(lastPayload.Payload.SenderUsername);

            if (success)
            {
                return (true, $"Successfully gave a thumbs up to '{lastPayload.Payload.SenderUsername}'.");
            }
            else
            {
                return (false, $"Failed to give a thumbs up to '{lastPayload.Payload.SenderUsername}'. Please check the logs.");
            }
        }
    }
}