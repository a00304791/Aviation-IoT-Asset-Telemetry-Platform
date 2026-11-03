namespace IoTPlatform.Application.DTOs;

public class MaintenanceLogDto
{
    public int Id { get; set; }

    public int DeviceAssetId { get; set; }

    public string MaintenanceType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime PerformedAt { get; set; }

    public string TechnicianName { get; set; } = string.Empty;
}