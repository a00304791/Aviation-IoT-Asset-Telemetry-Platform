namespace IoTPlatform.Domain.Exceptions;

public class InvalidOperatingRangeException : Exception
{
    public InvalidOperatingRangeException(string message)
        : base(message)
    {
    }
}