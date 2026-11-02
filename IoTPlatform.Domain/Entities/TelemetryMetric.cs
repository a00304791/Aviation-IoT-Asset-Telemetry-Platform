namespace IoTPlatform.Domain.Entities;

public class TelemetryMetric
{
    public int Id { get; private set; }
    public int DeviceAssetId { get; private set; }
    public TelemetryMetricType MetricType { get; private set; }
    public decimal Value { get; private set; }
    public string Unit { get; private set; } = string.Empty;
    public DateTime RecordedAt { get; private set; }

    private TelemetryMetric()
    {
    }

    public TelemetryMetric(
        int deviceAssetId,
        TelemetryMetricType metricType,
        decimal value,
        string unit,
        DateTime recordedAt)
    {
        if (deviceAssetId <= 0)
            throw new ArgumentException(
                "Device asset ID must be greater than zero.",
                nameof(deviceAssetId));

        if (!Enum.IsDefined(metricType))
            throw new ArgumentException(
                "Invalid telemetry metric type.",
                nameof(metricType));

        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentException(
                "Measurement unit is required.",
                nameof(unit));

        DeviceAssetId = deviceAssetId;
        MetricType = metricType;
        Value = value;
        Unit = unit;
        RecordedAt = recordedAt;
    }
}