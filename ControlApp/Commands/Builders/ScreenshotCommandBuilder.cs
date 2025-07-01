namespace ControlApp.Commands.Builders;

public class ScreenshotCommandBuilder() : SimpleCommandBuilder("Screenshot Command") {
    public override Command BuildCommand(Panel inputPanel) {
        return new ScreenshotCommand(EMPTY_CONTENT_STRING);
    }
}