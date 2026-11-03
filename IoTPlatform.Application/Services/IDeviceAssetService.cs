using IoTPlatform.Application.DTOs;

namespace IoTPlatform.Application.Services;

public interface IDeviceAssetService
{
    Task<DeviceAssetDto?> GetByIdAsync(int id);

    Task<IReadOnlyList<DeviceAssetDto>> GetAllAsync();

    Task<DeviceAssetDto> CreateAsync(DeviceAssetDto deviceAsset);

    Task UpdateAsync(int id, DeviceAssetDto deviceAsset);

    Task DeleteAsync(int id);
}