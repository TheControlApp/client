namespace ControlApp.Commands.Builders;

public abstract class CommandBuilder(string displayName) {
    public string DisplayName { get; } = displayName;
    
    public abstract void ConfigureInputPanel(Panel inputPanel);

    public abstract CommandStructure BuildCommand(Panel inputPanel);
}