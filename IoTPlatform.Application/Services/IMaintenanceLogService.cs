using IoTPlatform.Application.DTOs;

namespace IoTPlatform.Application.Services;

public interface IMaintenanceLogService
{
    Task<MaintenanceLogDto?> GetByIdAsync(int id);

    Task<IReadOnlyList<MaintenanceLogDto>> GetByDeviceAssetIdAsync(
        int deviceAssetId);

    Task<IReadOnlyList<MaintenanceLogDto>> GetAllAsync();

    Task<MaintenanceLogDto> CreateAsync(MaintenanceLogDto maintenanceLog);

    Task DeleteAsync(int id);
}