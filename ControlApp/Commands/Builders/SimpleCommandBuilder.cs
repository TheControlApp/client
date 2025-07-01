namespace ControlApp.Commands.Builders;

public abstract class SimpleCommandBuilder(string displayName) : CommandBuilder(displayName) {
    protected readonly string EMPTY_CONTENT_STRING = "Yes";

    public override void ConfigureInputPanel(Panel inputPanel) {}
}