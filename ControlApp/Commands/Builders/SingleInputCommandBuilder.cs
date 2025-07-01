namespace ControlApp.Commands.Builders;

public abstract class SingleInputCommandBuilder(string displayName, string labelText) : CommandBuilder(displayName) {
    private string labelText = labelText;
    
    public override void ConfigureInputPanel(Panel inputPanel) {
        Label upperLabel = (Label)inputPanel.Controls["upperLabel"];
        upperLabel.Text = labelText;
        upperLabel.Show();
        inputPanel.Controls["upperTextBox"].Show();
    }
}