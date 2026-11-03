using IoTPlatform.Domain.Entities;

namespace IoTPlatform.Application.Interfaces;

public interface ITelemetryMetricRepository
{
    Task<TelemetryMetric?> GetByIdAsync(int id);

    Task<IReadOnlyList<TelemetryMetric>> GetByDeviceAssetIdAsync(
        int deviceAssetId);

    Task<IReadOnlyList<TelemetryMetric>> GetAllAsync();

    Task AddAsync(TelemetryMetric metric);

    Task DeleteAsync(int id);
}