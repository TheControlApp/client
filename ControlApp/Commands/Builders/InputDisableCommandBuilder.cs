namespace ControlApp.Commands.Builders;

public class InputDisableCommandBuilder() : SimpleCommandBuilder("Input Disable Command") {
    public override Command BuildCommand(Panel inputPanel) {
        return new InputDisableCommand(EMPTY_CONTENT_STRING);
    }
}