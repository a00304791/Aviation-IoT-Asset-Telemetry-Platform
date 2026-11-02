namespace IoTPlatform.Domain.Exceptions;

public class InvalidSerialNumberException : Exception
{
    public InvalidSerialNumberException(string message)
        : base(message)
    {
    }
}