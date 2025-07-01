namespace ControlApp.Commands.Builders;

public class WebcamCommandBuilder() : SimpleCommandBuilder("Webcam Command") {
    public override Command BuildCommand(Panel inputPanel) {
        return new WebcamCommand(EMPTY_CONTENT_STRING);
    }
}