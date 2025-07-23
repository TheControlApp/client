using System.Security.Policy;
using System.Text.Json;

namespace ControlApp.Commands.Builders;

public class WebcamCommandBuilder() : SimpleCommandBuilder("Webcam Command") {
    public override CommandStructure BuildCommand(Panel inputPanel) {
        return new CommandStructure
        {
            Type = CommandCodes.Webcam
        };
    }
}