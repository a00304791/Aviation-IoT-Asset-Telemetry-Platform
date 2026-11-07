
using IoTPlatform.Domain.Entities;
using IoTPlatform.Domain.ValueObjects;
using IoTPlatform.Infrastructure.Data;
using IoTPlatform.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IoTPlatform.UnitTests.Repositories;

[TestClass]
public class AlertRuleRepositoryTests
{
    private IoTPlatformDbContext _context = null!;
    private AlertRuleRepository _repository = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<IoTPlatformDbContext>()
            .UseSqlite("Data Source=:memory:")
            .Options;

        _context = new IoTPlatformDbContext(options);
        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();

        _repository = new AlertRuleRepository(_context);
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

    private static AlertRule CreateAlertRule(int deviceAssetId)
    {
        return new AlertRule(
            deviceAssetId,
            "High Temperature Alert",
            TelemetryMetricType.Temperature,
            new OperatingRange(10, 150));
    }

    [TestMethod]
    public async Task AddAsync_ShouldSaveAlertRule()
    {
        var asset = await CreateAssetAsync();
        var alertRule = CreateAlertRule(asset.Id);

        await _repository.AddAsync(alertRule);

        Assert.AreEqual(1, await _context.AlertRules.CountAsync());
    }

    [TestMethod]
    public async Task GetByIdAsync_ShouldReturnAlertRule()
    {
        var asset = await CreateAssetAsync();
        var alertRule = CreateAlertRule(asset.Id);

        await _repository.AddAsync(alertRule);

        var result = await _repository.GetByIdAsync(alertRule.Id);

        Assert.IsNotNull(result);
        Assert.AreEqual(alertRule.Name, result.Name);
    }

    [TestMethod]
    public async Task GetByIdAsync_ShouldReturnNull_WhenAlertRuleDoesNotExist()
    {
        var result = await _repository.GetByIdAsync(999);

        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task GetByDeviceAssetIdAsync_ShouldReturnMatchingAlertRules()
    {
        var asset = await CreateAssetAsync();

        await _repository.AddAsync(CreateAlertRule(asset.Id));
        await _repository.AddAsync(CreateAlertRule(asset.Id));

        var result = await _repository.GetByDeviceAssetIdAsync(asset.Id);

        Assert.HasCount(2, result);
    }

    [TestMethod]
    public async Task GetAllAsync_ShouldReturnAllAlertRules()
    {
        var asset = await CreateAssetAsync();

        await _repository.AddAsync(CreateAlertRule(asset.Id));
        await _repository.AddAsync(CreateAlertRule(asset.Id));

        var result = await _repository.GetAllAsync();

        Assert.HasCount(2, result);
    }

    [TestMethod]
    public async Task UpdateAsync_ShouldSaveDisabledAlertRule()
    {
        var asset = await CreateAssetAsync();
        var alertRule = CreateAlertRule(asset.Id);

        await _repository.AddAsync(alertRule);

        alertRule.Disable();

        await _repository.UpdateAsync(alertRule);

        var result = await _repository.GetByIdAsync(alertRule.Id);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsEnabled);
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldRemoveAlertRule()
    {
        var asset = await CreateAssetAsync();
        var alertRule = CreateAlertRule(asset.Id);

        await _repository.AddAsync(alertRule);
        await _repository.DeleteAsync(alertRule.Id);

        Assert.AreEqual(0, await _context.AlertRules.CountAsync());
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldNotFail_WhenAlertRuleDoesNotExist()
    {
        await _repository.DeleteAsync(999);

        Assert.AreEqual(0, await _context.AlertRules.CountAsync());
    }
}