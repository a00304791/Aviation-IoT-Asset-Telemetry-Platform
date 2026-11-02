namespace IoTPlatform.Domain.Entities;

public class MaintenanceLog
{
    public int Id { get; private set; }

    public int DeviceAssetId { get; private set; }

    public string MaintenanceType { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public DateTime PerformedAt { get; private set; }

    public string TechnicianName { get; private set; } = string.Empty;

    // Required by Entity Framework Core
    private MaintenanceLog()
    {
    }

    public MaintenanceLog(
        int deviceAssetId,
        string maintenanceType,
        string description,
        DateTime performedAt,
        string technicianName)
    {
        if (deviceAssetId <= 0)
            throw new ArgumentException(
                "Device asset ID must be greater than zero.",
                nameof(deviceAssetId));

        if (string.IsNullOrWhiteSpace(maintenanceType))
            throw new ArgumentException(
                "Maintenance type is required.",
                nameof(maintenanceType));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException(
                "Maintenance description is required.",
                nameof(description));

        if (string.IsNullOrWhiteSpace(technicianName))
            throw new ArgumentException(
                "Technician name is required.",
                nameof(technicianName));

        DeviceAssetId = deviceAssetId;
        MaintenanceType = maintenanceType;
        Description = description;
        PerformedAt = performedAt;
        TechnicianName = technicianName;
    }
}