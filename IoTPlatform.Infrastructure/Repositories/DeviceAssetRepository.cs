using IoTPlatform.Application.Interfaces;
using IoTPlatform.Domain.Entities;
using IoTPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IoTPlatform.Infrastructure.Repositories;

public class DeviceAssetRepository : IDeviceAssetRepository
{
    private readonly IoTPlatformDbContext _context;

    public DeviceAssetRepository(IoTPlatformDbContext context)
    {
        _context = context;
    }

    public async Task<DeviceAsset?> GetByIdAsync(int id)
    {
        return await _context.DeviceAssets
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<DeviceAsset?> GetBySerialNumberAsync(string serialNumber)
    {
        var assets = await _context.DeviceAssets.ToListAsync();
        return assets.FirstOrDefault(d => d.SerialNumber.Value == serialNumber);
    }

    public async Task<IReadOnlyList<DeviceAsset>> GetAllAsync()
    {
        return await _context.DeviceAssets.ToListAsync();
    }

    public async Task AddAsync(DeviceAsset deviceAsset)
    {
        await _context.DeviceAssets.AddAsync(deviceAsset);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(DeviceAsset deviceAsset)
    {
        _context.DeviceAssets.Update(deviceAsset);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var deviceAsset = await _context.DeviceAssets.FindAsync(id);

        if (deviceAsset is null)
            return;

        _context.DeviceAssets.Remove(deviceAsset);
        await _context.SaveChangesAsync();
    }
}