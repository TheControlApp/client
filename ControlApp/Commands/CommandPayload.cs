using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using ControlApp.Utils;

namespace ControlApp.Commands
{
    // Represents the root JSON object
    public class CommandPayload
    {
        [JsonPropertyName("commandId")]
        public int CommandId { get; set; }
        [JsonPropertyName("sender")]
        public string SenderUsername { get; set; }
        [JsonPropertyName("commands")]
        public List<CommandStructure> Commands { get; set; }

        public void Execute()
        {
            int commandRan = 0;
            foreach (CommandStructure command in Commands)
            {
                Command c = Command.ParseJsonCommand(command);
                if(c == null)
                {
                    Utilities.LogWarning($"A command couldn't run in payload {CommandId}");
                    continue;
                }
                c.Execute(SenderUsername, command.Content);
                commandRan++;
            }
            Utilities.LogInfo($"{commandRan} commands ran in payload {CommandId}");
        }
    }

    // Represents a single command within the "commands" array
    public class CommandStructure
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("content")]
        public JsonElement Content { get; set; }
    }
}