using IoTPlatform.Application.DTOs;

namespace IoTPlatform.Application.Services;

public interface ITelemetryMetricService
{
    Task<TelemetryMetricDto?> GetByIdAsync(int id);

    Task<IReadOnlyList<TelemetryMetricDto>> GetByDeviceAssetIdAsync(
        int deviceAssetId);

    Task<IReadOnlyList<TelemetryMetricDto>> GetAllAsync();

    Task<TelemetryMetricDto> CreateAsync(TelemetryMetricDto metric);

    Task DeleteAsync(int id);
}