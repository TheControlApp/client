namespace ControlApp.Commands.Builders;

public class MouseDisableCommandBuilder() : SimpleCommandBuilder("Mouse Disable Command") {
    public override CommandStructure BuildCommand(Panel inputPanel) {
        return new CommandStructure { Type = CommandCodes.MouseDisable };
    }
}