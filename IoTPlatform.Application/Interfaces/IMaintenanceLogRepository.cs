using IoTPlatform.Domain.Entities;

namespace IoTPlatform.Application.Interfaces;

public interface IMaintenanceLogRepository
{
    Task<MaintenanceLog?> GetByIdAsync(int id);

    Task<IReadOnlyList<MaintenanceLog>> GetByDeviceAssetIdAsync(
        int deviceAssetId);

    Task<IReadOnlyList<MaintenanceLog>> GetAllAsync();

    Task AddAsync(MaintenanceLog maintenanceLog);

    Task DeleteAsync(int id);
}