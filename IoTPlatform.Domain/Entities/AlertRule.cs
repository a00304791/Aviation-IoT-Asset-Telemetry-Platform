using IoTPlatform.Domain.ValueObjects;

namespace IoTPlatform.Domain.Entities;

public class AlertRule
{
    public int Id { get; private set; }

    public int DeviceAssetId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public TelemetryMetricType MetricType { get; private set; }

    public OperatingRange ThresholdRange { get; private set; } = null!;

    public bool IsEnabled { get; private set; }

    // Required by Entity Framework Core
    private AlertRule()
    {
    }

    public AlertRule(
        int deviceAssetId,
        string name,
        TelemetryMetricType metricType,
        OperatingRange thresholdRange)
    {
        if (deviceAssetId <= 0)
            throw new ArgumentException(
                "Device asset ID must be greater than zero.",
                nameof(deviceAssetId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Alert rule name is required.",
                nameof(name));

        if (!Enum.IsDefined(metricType))
            throw new ArgumentException(
                "Invalid telemetry metric type.",
                nameof(metricType));

        if (thresholdRange is null)
            throw new ArgumentNullException(nameof(thresholdRange));

        DeviceAssetId = deviceAssetId;
        Name = name;
        MetricType = metricType;
        ThresholdRange = thresholdRange;
        IsEnabled = true;
    }

    public bool IsOutsideThreshold(decimal value)
    {
        return !ThresholdRange.Contains(value);
    }

    public void Enable()
    {
        IsEnabled = true;
    }

    public void Disable()
    {
        IsEnabled = false;
    }
}