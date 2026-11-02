using System.Text.RegularExpressions;
using IoTPlatform.Domain.Exceptions;

namespace IoTPlatform.Domain.ValueObjects;

public sealed class SerialNumber
{
    private static readonly Regex SerialNumberPattern =
        new(@"^[A-Z]{3}-[0-9]{5}$", RegexOptions.Compiled);

    public string Value { get; }

    public SerialNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value) ||
            !SerialNumberPattern.IsMatch(value))
        {
            throw new InvalidSerialNumberException(
                "Serial number must follow the format AAA-12345 (three uppercase letters, a hyphen, and five digits).");
        }

        Value = value;
    }

    public override string ToString() => Value;

    public override bool Equals(object? obj)
    {
        return obj is SerialNumber other && Value == other.Value;
    }

    public override int GetHashCode() => Value.GetHashCode();
}