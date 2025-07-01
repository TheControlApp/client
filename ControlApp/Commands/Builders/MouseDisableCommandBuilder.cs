namespace ControlApp.Commands.Builders;

public class MouseDisableCommandBuilder() : SimpleCommandBuilder("Mouse Disable Command") {
    public override Command BuildCommand(Panel inputPanel) {
        return new MouseDisableCommand(EMPTY_CONTENT_STRING);
    }
}