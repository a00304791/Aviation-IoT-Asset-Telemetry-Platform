using IoTPlatform.Domain.Exceptions;

namespace IoTPlatform.Domain.ValueObjects;

public sealed class OperatingRange
{
    public decimal Minimum { get; }
    public decimal Maximum { get; }

    public OperatingRange(decimal minimum, decimal maximum)
    {
        if (minimum <= 0)
        {
            throw new InvalidOperatingRangeException(
                "Minimum operating limit must be greater than zero.");
        }

        if (maximum <= 0)
        {
            throw new InvalidOperatingRangeException(
                "Maximum operating limit must be greater than zero.");
        }

        if (maximum <= minimum)
        {
            throw new InvalidOperatingRangeException(
                "Maximum operating limit must be greater than minimum.");
        }

        Minimum = minimum;
        Maximum = maximum;
    }

    public bool Contains(decimal value)
    {
        return value >= Minimum && value <= Maximum;
    }
}