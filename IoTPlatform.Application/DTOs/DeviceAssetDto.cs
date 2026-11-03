namespace IoTPlatform.Application.DTOs;

public class DeviceAssetDto
{
    public int Id { get; set; }

    public string SerialNumber { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string ComponentType { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public decimal MinimumOperatingTemperature { get; set; }

    public decimal MaximumOperatingTemperature { get; set; }
}