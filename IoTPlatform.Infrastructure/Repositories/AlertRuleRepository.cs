using IoTPlatform.Application.Interfaces;
using IoTPlatform.Domain.Entities;
using IoTPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IoTPlatform.Infrastructure.Repositories;

public class AlertRuleRepository : IAlertRuleRepository
{
    private readonly IoTPlatformDbContext _context;

    public AlertRuleRepository(IoTPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<AlertRule?> GetByIdAsync(int id)
    {
        return await _context.AlertRules
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IReadOnlyList<AlertRule>> GetByDeviceAssetIdAsync(int deviceAssetId)
    {
        return await _context.AlertRules
            .Where(a => a.DeviceAssetId == deviceAssetId)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<AlertRule>> GetAllAsync()
    {
        return await _context.AlertRules.ToListAsync();
    }

    public async Task AddAsync(AlertRule alertRule)
    {
        await _context.AlertRules.AddAsync(alertRule);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AlertRule alertRule)
    {
        _context.AlertRules.Update(alertRule);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var alertRule = await _context.AlertRules.FindAsync(id);

        if (alertRule is null)
            return;

        _context.AlertRules.Remove(alertRule);
        await _context.SaveChangesAsync();
    }
}