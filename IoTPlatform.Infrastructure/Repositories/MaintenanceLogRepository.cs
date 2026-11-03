using IoTPlatform.Application.Interfaces;
using IoTPlatform.Domain.Entities;
using IoTPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IoTPlatform.Infrastructure.Repositories;

public class MaintenanceLogRepository : IMaintenanceLogRepository
{
    private readonly IoTPlatformDbContext _context;

    public MaintenanceLogRepository(IoTPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<MaintenanceLog?> GetByIdAsync(int id)
    {
        return await _context.MaintenanceLogs
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IReadOnlyList<MaintenanceLog>> GetByDeviceAssetIdAsync(int deviceAssetId)
    {
        return await _context.MaintenanceLogs
            .Where(m => m.DeviceAssetId == deviceAssetId)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<MaintenanceLog>> GetAllAsync()
    {
        return await _context.MaintenanceLogs.ToListAsync();
    }

    public async Task AddAsync(MaintenanceLog maintenanceLog)
    {
        await _context.MaintenanceLogs.AddAsync(maintenanceLog);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var maintenanceLog = await _context.MaintenanceLogs.FindAsync(id);

        if (maintenanceLog is null)
            return;

        _context.MaintenanceLogs.Remove(maintenanceLog);
        await _context.SaveChangesAsync();
    }
}