namespace ControlApp.Commands.Builders;

public class DummyCommandBuilder(string displayName) : SimpleCommandBuilder(displayName) {
    public override CommandStructure? BuildCommand(Panel inputPanel) {
        return null;
    }
}