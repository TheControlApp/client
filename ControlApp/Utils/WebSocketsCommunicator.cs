// ==== File: ControlApp\Utilities\WebSocketsCommunicator.cs ====

using ControlApp.Commands;
using ControlApp.Services;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace ControlApp.Utils;

/// <summary>
/// Manages a persistent WebSocket connection to the backend for real-time command communication.
/// This class is designed to be a static utility, maintaining a single connection for the application lifecycle.
/// </summary>
public static class WebSocketsCommunicator
{
    private static ClientWebSocket _webSocket;
    private static readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
    private static readonly string _webSocketUrl = "wss://www.thecontrolapp.co.uk/ws"; // Secure WebSocket URL

    /// <summary>
    /// Fires when a new command is received from the server.
    /// The payload is the fully parsed Command object.
    /// </summary>
    public static event Action<CommandPayload> CommandReceived;

    /// <summary>
    /// Fires when the WebSocket connection is successfully established.
    /// </summary>
    public static event Action Connected;

    /// <summary>
    /// Fires when the WebSocket connection is closed or lost.
    /// </summary>
    public static event Action Disconnected;

    /// <summary>
    /// Gets a value indicating whether the WebSocket is currently connected.
    /// </summary>
    public static bool IsConnected => _webSocket?.State == WebSocketState.Open;

    /// <summary>
    /// Initiates a connection to the WebSocket server.
    /// Authenticates using the stored JWT.
    /// </summary>
    public static async Task ConnectAsync()
    {
        if (IsConnected)
        {
            Utilities.LogInfo("WebSocket is already connected.");
            return;
        }

        try
        {
            _webSocket = new ClientWebSocket();
            string token = SecureTokenStorage.ReadToken();

            if (string.IsNullOrEmpty(token))
            {
                Utilities.LogError("Cannot connect WebSocket: No authentication token found.");
                return;
            }

            // Add the authentication token to the connection request header
            _webSocket.Options.SetRequestHeader("Authorization", $"Bearer {token}");

            Utilities.LogInfo($"Connecting to WebSocket server at {_webSocketUrl}...");
            await _webSocket.ConnectAsync(new Uri(_webSocketUrl), _cancellationTokenSource.Token);

            if (IsConnected)
            {
                Utilities.LogInfo("WebSocket connection established.");
                Connected?.Invoke();
                // Start listening for messages on a background thread
                _ = Task.Run(ListenForMessages);
            }
        }
        catch (Exception ex)
        {
            Utilities.LogError($"WebSocket connection failed: {ex.Message}");
            Disconnected?.Invoke();
        }
    }

    /// <summary>
    /// Continuously listens for incoming messages from the server.
    /// Should be run as a background task.
    /// </summary>
    private static async Task ListenForMessages()
    {
        var buffer = new byte[1024 * 4];

        try
        {
            while (_webSocket.State == WebSocketState.Open)
            {
                using var ms = new MemoryStream();
                WebSocketReceiveResult result;
                do
                {
                    result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), _cancellationTokenSource.Token);
                    await ms.WriteAsync(buffer.AsMemory(0, result.Count), new CancellationToken());
                } while (!result.EndOfMessage);

                ms.Seek(0, SeekOrigin.Begin);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    string messageJson = Encoding.UTF8.GetString(ms.ToArray());
                    HandleIncomingMessage(messageJson);
                }
            }
        }
        catch (WebSocketException wsex)
        {
            Utilities.LogWarning($"WebSocket exception during listen: {wsex.Message}. Connection likely closed.");
        }
        catch (Exception ex)
        {
            Utilities.LogError($"Error in WebSocket listener: {ex.Message}");
        }
        finally
        {
            if (_webSocket.State != WebSocketState.Closed)
            {
                await DisconnectAsync();
            }
            else
            {
                Disconnected?.Invoke();
            }
        }
    }

    /// <summary>
    /// Parses an incoming JSON message and raises the appropriate event.
    /// </summary>
    private static void HandleIncomingMessage(string messageJson)
    {
        try
        {
            var message = JsonSerializer.Deserialize<WebSocketMessage>(messageJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Backend will now send a "command_batch" type
            if (message?.Type?.ToLower() == "command_batch")
            {
                Utilities.LogInfo($"Command batch received via WebSocket from sender: {message.Sender}");

                // Deserialize the JSON payload to get the list of command structures
                var payload = JsonSerializer.Deserialize<CommandPayload>(message.Payload);
                if (payload != null) 
                {
                    if (AccountService.CurrentUser != null && AccountService.CurrentUser.BlackList.Contains(payload.SenderUsername))
                    {
                        Utilities.LogWarning($"Received command from user {payload.SenderUsername}, skipping !");
                        return;
                    }
                    CommandManager.EnqueuePayload(payload);
                }
            }
            else
            {
                Utilities.LogInfo($"Received unhandled WebSocket message: {messageJson}");
            }
        }
        catch (Exception ex)
        {
            Utilities.LogError($"Error handling incoming WebSocket message: {ex.Message}");
        }
    }

    /// <summary>
    /// Sends a command to the backend over the WebSocket.
    /// </summary>
    public static async Task SendCommandAsync(string destUser, List<CommandStructure> commands, bool groupSend)
    {
        if (!IsConnected)
        {
            Utilities.LogWarning("Cannot send command, WebSocket is not connected.");
            return;
        }

        try
        {
            // Create the root payload object
            var payload = new CommandPayload { Commands = commands };
            string jsonPayload = JsonSerializer.Serialize(payload);

            var message = new WebSocketMessage
            {
                Type = "command_batch", // A new type for sending commands
                Recipient = destUser,
                Payload = jsonPayload,
                DestinationType = groupSend ? "group" : "user",
            };

            string messageJson = JsonSerializer.Serialize(message);
            var buffer = Encoding.UTF8.GetBytes(messageJson);

            await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, _cancellationTokenSource.Token);
            Utilities.LogInfo($"Sent batch of {commands.Count} commands to '{destUser}'.");
        }
        catch (Exception ex)
        {
            Utilities.LogError($"Failed to send command via WebSocket: {ex.Message}");
        }
    }

    /// <summary>
    /// Notifies the server that a specific command payload has been executed.
    /// This is an "at-least-once" confirmation, allowing the server to track command state.
    /// This should be called after a payload is executed or re-executed.
    /// </summary>
    /// <param name="executedTask">The task containing the payload that was just executed. It must not be null.</param>
    public static async Task AcknowledgePayloadExecutionAsync(CommandPayloadTask executedTask)
    {
        // 1. Guard Clause: Ensure the application is in a valid state to send.
        if (!IsConnected)
        {
            Utilities.LogWarning($"Cannot acknowledge payload ID '{executedTask?.Payload.CommandId}'; WebSocket is not connected.");
            return;
        }

        // 2. Input Validation: Protect against programming errors from the calling code.
        if (executedTask == null)
        {
            Utilities.LogError("AcknowledgePayloadExecutionAsync was called with a null task. This indicates a bug in the CommandManager.");
            return;
        }

        // 3. Construct the Message: Create the specific JSON message object for this action.
        //    The 'type' tells the server how to interpret this message.
        //    The 'PayloadId' is the crucial piece of data the server needs.
        var message = new WebSocketMessage
        {
            Type = "ack_payload_executed",
            Payload = JsonSerializer.Serialize(executedTask.Payload)
        };

        // 4. Send the Message: Use the private helper to handle serialization and the actual send operation.
        string messageJson = JsonSerializer.Serialize(message);
        var buffer = Encoding.UTF8.GetBytes(messageJson);
        await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, _cancellationTokenSource.Token);

        // 5. Log the Action: Provide clear feedback in the logs for debugging purposes.
        Utilities.LogInfo($"Acknowledged execution of payload ID '{executedTask.Payload.CommandId}'.");
    }

    /// <summary>
    /// Notifies the server that all pending command payloads in the client's queue
    /// have been cleared and should be considered ignored. The server should use this
    /// information to prevent these payloads from being sent again on a future session.
    /// </summary>
    /// <param name="clearedPayloadIds">An enumerable collection of the unique IDs of the payloads that were cleared.</param>
    public static async Task AcknowledgeQueueClearedAsync(IEnumerable<int> clearedPayloadIds)
    {
        // 1. Guard Clause: Check for a valid connection.
        if (!IsConnected)
        {
            Utilities.LogWarning("Cannot acknowledge queue cleared; WebSocket is not connected.");
            return;
        }

        // 2. Guard Clause: Check for valid input. If the list is null or empty, there's nothing to report.
        if (clearedPayloadIds == null || !clearedPayloadIds.Any())
        {
            Utilities.LogInfo("AcknowledgeQueueClearedAsync was called with no payload IDs. Nothing to report to the server.");
            return;
        }

        // 3. Construct the Message: Create the specific JSON message object for this action.
        var message = new WebSocketMessage
        {
            Type = "ack_queue_cleared",
            Payload = JsonSerializer.Serialize(clearedPayloadIds)
        };

        // 4. Log the Action: This is important for debugging and tracing the application's flow.
        Utilities.LogInfo($"Notifying server that {clearedPayloadIds.Count()} payloads have been cleared by the user.");

        // 5. Send the Message: Use the centralized helper to handle the actual WebSocket send operation.
        string messageJson = JsonSerializer.Serialize(message);
        var buffer = Encoding.UTF8.GetBytes(messageJson);
        await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, _cancellationTokenSource.Token);
    }

    /// <summary>
    /// Closes the WebSocket connection gracefully.
    /// </summary>
    public static async Task DisconnectAsync()
    {
        if (_webSocket != null && (IsConnected || _webSocket.State == WebSocketState.CloseSent))
        {
            try
            {
                await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client disconnecting", _cancellationTokenSource.Token);
            }
            catch (Exception ex)
            {
                Utilities.LogWarning($"Exception while closing WebSocket: {ex.Message}");
            }
        }

        _webSocket?.Dispose();
        _webSocket = null;
        Utilities.LogInfo("WebSocket connection closed.");
        Disconnected?.Invoke();
    }

    // Private record to model the structure of WebSocket JSON messages
    private record WebSocketMessage
    {
        public string Type { get; set; } // e.g., "command", "sendCommand"
        public string Payload { get; set; } // Encrypted command string
        public string Sender { get; set; } // For incoming messages
        public string DestinationType { get; set; } // For outgoing: "user" or "group"
        public string Recipient { get; set; } // For outgoing: the username or group name
    }
}