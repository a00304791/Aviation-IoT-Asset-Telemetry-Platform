using IoTPlatform.Application.DTOs;

namespace IoTPlatform.Application.Services;

public interface IAlertRuleService
{
    Task<AlertRuleDto?> GetByIdAsync(int id);

    Task<IReadOnlyList<AlertRuleDto>> GetByDeviceAssetIdAsync(
        int deviceAssetId);

    Task<IReadOnlyList<AlertRuleDto>> GetAllAsync();

    Task<AlertRuleDto> CreateAsync(AlertRuleDto alertRule);

    Task UpdateAsync(int id, AlertRuleDto alertRule);

    Task DeleteAsync(int id);
}