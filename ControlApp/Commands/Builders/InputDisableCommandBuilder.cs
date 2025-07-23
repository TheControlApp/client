namespace ControlApp.Commands.Builders;

public class InputDisableCommandBuilder() : SimpleCommandBuilder("Input Disable Command") {
    public override CommandStructure BuildCommand(Panel inputPanel) {
        return new CommandStructure { Type = CommandCodes.InputDisable };
    }
}