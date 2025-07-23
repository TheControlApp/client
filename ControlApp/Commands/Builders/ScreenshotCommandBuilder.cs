using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace ControlApp.Commands.Builders;

public class ScreenshotCommandBuilder() : SimpleCommandBuilder("Screenshot Command") {
    public override CommandStructure BuildCommand(Panel inputPanel) {
        return new CommandStructure
        {
            Type = CommandCodes.Screenshot
        };
    }
}