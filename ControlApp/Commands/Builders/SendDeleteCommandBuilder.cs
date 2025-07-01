namespace ControlApp.Commands.Builders;

public class SendDeleteCommandBuilder() : SimpleCommandBuilder("Send or Delete Command") {
    public override Command BuildCommand(Panel inputPanel) {
        return new SendDeleteCommand(EMPTY_CONTENT_STRING);
    }
}