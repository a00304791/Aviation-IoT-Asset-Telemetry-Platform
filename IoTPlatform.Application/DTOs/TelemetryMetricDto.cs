namespace IoTPlatform.Application.DTOs;

public class TelemetryMetricDto
{
    public int Id { get; set; }

    public int DeviceAssetId { get; set; }

    public string MetricType { get; set; } = string.Empty;

    public decimal Value { get; set; }

    public string Unit { get; set; } = string.Empty;

    public DateTime RecordedAt { get; set; }
}