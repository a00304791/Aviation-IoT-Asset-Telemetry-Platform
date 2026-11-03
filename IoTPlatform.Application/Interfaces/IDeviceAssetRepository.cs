using IoTPlatform.Domain.Entities;

namespace IoTPlatform.Application.Interfaces;

public interface IDeviceAssetRepository
{
    Task<DeviceAsset?> GetByIdAsync(int id);

    Task<DeviceAsset?> GetBySerialNumberAsync(string serialNumber);

    Task<IReadOnlyList<DeviceAsset>> GetAllAsync();

    Task AddAsync(DeviceAsset deviceAsset);

    Task UpdateAsync(DeviceAsset deviceAsset);

    Task DeleteAsync(int id);
}