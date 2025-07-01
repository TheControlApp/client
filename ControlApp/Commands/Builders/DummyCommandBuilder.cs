namespace ControlApp.Commands.Builders;

public class DummyCommandBuilder(string displayName) : SimpleCommandBuilder(displayName) {
    public override Command? BuildCommand(Panel inputPanel) {
        return null;
    }
}