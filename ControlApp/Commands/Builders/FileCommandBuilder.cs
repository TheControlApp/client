namespace ControlApp.Commands.Builders;

public abstract class FileCommandBuilder(string displayName, string labelText) : CommandBuilder(displayName) {
    private string labelText = labelText;
    
    public override void ConfigureInputPanel(Panel inputPanel) {
        Label upperLabel = (Label) inputPanel.Controls["upperLabel"];
        upperLabel.Text = labelText;
        RadioButton fileRadioButton = (RadioButton) inputPanel.Controls["fileRadioButton"];
        RadioButton urlRadioButton = (RadioButton) inputPanel.Controls["urlRadioButton"];
        upperLabel.Visible = fileRadioButton.Checked || urlRadioButton.Checked;
        fileRadioButton.Show();
        urlRadioButton.Show();
        inputPanel.Controls["fileNameTextBox"].Visible = fileRadioButton.Checked;
        inputPanel.Controls["fileUploadButton"].Visible = fileRadioButton.Checked;
        inputPanel.Controls["upperTextBox"].Visible = urlRadioButton.Checked;
    }
}