using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace ControlApp.Commands.Builders;

public class SendDeleteCommandBuilder() : SimpleCommandBuilder("Send or Delete Command") {
    public override CommandStructure BuildCommand(Panel inputPanel) {
        return new CommandStructure
        {
            Type = CommandCodes.SendDelete
        };
    }
}