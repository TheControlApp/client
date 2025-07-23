namespace ControlApp.Exceptions;

public class ServerApiException : Exception
{
    public ServerApiException()
    {
    }

    public ServerApiException(string? message) : base(message)
    {
    }
}
