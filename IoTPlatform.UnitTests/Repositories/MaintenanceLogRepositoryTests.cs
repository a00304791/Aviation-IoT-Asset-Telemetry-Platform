
using IoTPlatform.Domain.Entities;
using IoTPlatform.Domain.ValueObjects;
using IoTPlatform.Infrastructure.Data;
using IoTPlatform.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IoTPlatform.UnitTests.Repositories;

[TestClass]
public class MaintenanceLogRepositoryTests
{
    private IoTPlatformDbContext _context = null!;
    private MaintenanceLogRepository _repository = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<IoTPlatformDbContext>()
            .UseSqlite("Data Source=:memory:")
            .Options;

        _context = new IoTPlatformDbContext(options);
        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();

        _repository = new MaintenanceLogRepository(_context);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.CloseConnection();
        _context.Dispose();
    }

    private async Task<DeviceAsset> CreateAssetAsync()
    {
        var asset = new DeviceAsset(
            new SerialNumber("ENG-12345"),
            "Test Engine",
            "Engine",
            new OperatingRange(20, 120));

        await _context.DeviceAssets.AddAsync(asset);
        await _context.SaveChangesAsync();

        return asset;
    }

    private static MaintenanceLog CreateMaintenanceLog(int deviceAssetId)
    {
        return new MaintenanceLog(
            deviceAssetId,
            "Inspection",
            "Routine engine inspection.",
            DateTime.UtcNow,
            "John Smith");
    }

    [TestMethod]
    public async Task AddAsync_ShouldSaveMaintenanceLog()
    {
        var asset = await CreateAssetAsync();
        var log = CreateMaintenanceLog(asset.Id);

        await _repository.AddAsync(log);

        Assert.AreEqual(1, await _context.MaintenanceLogs.CountAsync());
    }

    [TestMethod]
    public async Task GetByIdAsync_ShouldReturnMaintenanceLog()
    {
        var asset = await CreateAssetAsync();
        var log = CreateMaintenanceLog(asset.Id);

        await _repository.AddAsync(log);

        var result = await _repository.GetByIdAsync(log.Id);

        Assert.IsNotNull(result);
        Assert.AreEqual(log.Description, result.Description);
    }

    [TestMethod]
    public async Task GetByIdAsync_ShouldReturnNull_WhenLogDoesNotExist()
    {
        var result = await _repository.GetByIdAsync(999);

        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task GetByDeviceAssetIdAsync_ShouldReturnMatchingLogs()
    {
        var asset = await CreateAssetAsync();

        await _repository.AddAsync(CreateMaintenanceLog(asset.Id));
        await _repository.AddAsync(CreateMaintenanceLog(asset.Id));

        var result = await _repository.GetByDeviceAssetIdAsync(asset.Id);

        Assert.HasCount(2, result);
    }

    [TestMethod]
    public async Task GetAllAsync_ShouldReturnAllMaintenanceLogs()
    {
        var asset = await CreateAssetAsync();

        await _repository.AddAsync(CreateMaintenanceLog(asset.Id));
        await _repository.AddAsync(CreateMaintenanceLog(asset.Id));

        var result = await _repository.GetAllAsync();

        Assert.HasCount(2, result);
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldRemoveMaintenanceLog()
    {
        var asset = await CreateAssetAsync();
        var log = CreateMaintenanceLog(asset.Id);

        await _repository.AddAsync(log);
        await _repository.DeleteAsync(log.Id);

        Assert.AreEqual(0, await _context.MaintenanceLogs.CountAsync());
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldNotFail_WhenLogDoesNotExist()
    {
        await _repository.DeleteAsync(999);

        Assert.AreEqual(0, await _context.MaintenanceLogs.CountAsync());
    }
}