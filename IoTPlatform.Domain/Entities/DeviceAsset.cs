using IoTPlatform.Domain.Exceptions;
using IoTPlatform.Domain.ValueObjects;

namespace IoTPlatform.Domain.Entities;

public class DeviceAsset
{
    public int Id { get; private set; }

    public SerialNumber SerialNumber { get; private set; } = null!;

    public string Name { get; private set; } = string.Empty;

    public string ComponentType { get; private set; } = string.Empty;

    public DeviceStatus Status { get; private set; }

    public OperatingRange OperatingTemperatureRange { get; private set; } = null!;

    // Required by Entity Framework Core
    private DeviceAsset()
    {
    }

    public DeviceAsset(
        SerialNumber serialNumber,
        string name,
        string componentType,
        OperatingRange operatingTemperatureRange)
    {
        if (serialNumber is null)
            throw new ArgumentNullException(nameof(serialNumber));

        if (operatingTemperatureRange is null)
            throw new ArgumentNullException(nameof(operatingTemperatureRange));

        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Asset name is required.");

        if (string.IsNullOrWhiteSpace(componentType))
            throw new DomainException("Component type is required.");

        SerialNumber = serialNumber;
        Name = name;
        ComponentType = componentType;
        OperatingTemperatureRange = operatingTemperatureRange;
        Status = DeviceStatus.Active;
    }

    public void ChangeStatus(DeviceStatus newStatus)
    {
        bool isValidTransition = (Status, newStatus) switch
        {
            (DeviceStatus.Active, DeviceStatus.UnderMaintenance) => true,
            (DeviceStatus.Active, DeviceStatus.Retired) => true,
            (DeviceStatus.UnderMaintenance, DeviceStatus.Active) => true,
            (DeviceStatus.UnderMaintenance, DeviceStatus.Retired) => true,
            _ => false
        };

        if (!Enum.IsDefined(newStatus) || !isValidTransition)
        {
            throw new InvalidStatusTransitionException(
                $"Cannot change asset status from {Status} to {newStatus}.");
        }

        Status = newStatus;
    }
}