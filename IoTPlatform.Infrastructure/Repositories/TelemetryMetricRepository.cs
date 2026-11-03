using IoTPlatform.Application.Interfaces;
using IoTPlatform.Domain.Entities;
using IoTPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IoTPlatform.Infrastructure.Repositories;

public class TelemetryMetricRepository : ITelemetryMetricRepository
{
    private readonly IoTPlatformDbContext _context;

    public TelemetryMetricRepository(IoTPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<TelemetryMetric?> GetByIdAsync(int id)
    {
        return await _context.TelemetryMetrics
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IReadOnlyList<TelemetryMetric>> GetByDeviceAssetIdAsync(int deviceAssetId)
    {
        return await _context.TelemetryMetrics
            .Where(t => t.DeviceAssetId == deviceAssetId)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<TelemetryMetric>> GetAllAsync()
    {
        return await _context.TelemetryMetrics.ToListAsync();
    }

    public async Task AddAsync(TelemetryMetric metric)
    {
        await _context.TelemetryMetrics.AddAsync(metric);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var metric = await _context.TelemetryMetrics.FindAsync(id);

        if (metric is null)
            return;

        _context.TelemetryMetrics.Remove(metric);
        await _context.SaveChangesAsync();
    }
}