namespace IoTPlatform.Application.DTOs;

public class AlertRuleDto
{
    public int Id { get; set; }

    public int DeviceAssetId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string MetricType { get; set; } = string.Empty;

    public decimal MinimumThreshold { get; set; }

    public decimal MaximumThreshold { get; set; }

    public bool IsEnabled { get; set; }
}