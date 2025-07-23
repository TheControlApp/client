namespace ControlApp.Exceptions;

public class ApiResponseParseException : Exception
{
    public ApiResponseParseException()
    {
    }

    public ApiResponseParseException(string? message) : base(message)
    {
    }
}
