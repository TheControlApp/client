using FluentFTP.Helpers;

namespace ControlApp.Commands.Builders;

public class SpinnerCommandBuilder() : SingleInputCommandBuilder("Spinner Command", "Spinner Options") {
    public override void ConfigureInputPanel(Panel inputPanel) {
        base.ConfigureInputPanel(inputPanel);
        TextBox upperTextBox = (TextBox) inputPanel.Controls["upperTextBox"];
        upperTextBox.Multiline = true;
        upperTextBox.Size = new Size(454, 212);
    }

    public override Command? BuildCommand(Panel inputPanel) {
        TextBox upperTextBox = (TextBox) inputPanel.Controls["upperTextBox"];
        if (upperTextBox.Lines.Length == 0 || upperTextBox.Lines.Length == 1) {
            MessageBox.Show("Please enter some options into the spinner box.");
            return null;
        }
        List<string> optionList = new List<string>();
        foreach (string line in upperTextBox.Lines) {
            if (Strings.IsNullOrWhiteSpace(line)) continue;
            optionList.Add(line);
        }
        if (upperTextBox.Lines.Length > 10) {
            MessageBox.Show("Too many options for spinner");
            return null;
        }
        string commandContent = String.Empty;
        foreach (string line in optionList) {
            commandContent += $"[{line}],";
        }
        if (!commandContent.IsBlank()) commandContent = commandContent.Remove(commandContent.Length - 1);
        upperTextBox.Clear();
        return new SpinnerCommand(commandContent);
    }
}