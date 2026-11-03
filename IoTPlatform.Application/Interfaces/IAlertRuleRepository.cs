using IoTPlatform.Domain.Entities;

namespace IoTPlatform.Application.Interfaces;

public interface IAlertRuleRepository
{
    Task<AlertRule?> GetByIdAsync(int id);

    Task<IReadOnlyList<AlertRule>> GetByDeviceAssetIdAsync(
        int deviceAssetId);

    Task<IReadOnlyList<AlertRule>> GetAllAsync();

    Task AddAsync(AlertRule alertRule);

    Task UpdateAsync(AlertRule alertRule);

    Task DeleteAsync(int id);
}