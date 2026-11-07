using IoTPlatform.Domain.Entities;
using IoTPlatform.Domain.ValueObjects;
using IoTPlatform.Infrastructure.Data;
using IoTPlatform.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IoTPlatform.UnitTests.Repositories;

[TestClass]
public class TelemetryMetricRepositoryTests
{
    private IoTPlatformDbContext _context = null!;
    private TelemetryMetricRepository _repository = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<IoTPlatformDbContext>()
            .UseSqlite("Data Source=:memory:")
            .Options;

        _context = new IoTPlatformDbContext(options);
        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();

        _repository = new TelemetryMetricRepository(_context);
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

    private static TelemetryMetric CreateMetric(
        int deviceAssetId,
        decimal value = 85)
    {
        return new TelemetryMetric(
            deviceAssetId,
            TelemetryMetricType.Temperature,
            value,
            "Celsius",
            DateTime.UtcNow);
    }

    [TestMethod]
    public async Task AddAsync_ShouldSaveTelemetryMetric()
    {
        var asset = await CreateAssetAsync();
        var metric = CreateMetric(asset.Id);

        await _repository.AddAsync(metric);

        Assert.AreEqual(1, await _context.TelemetryMetrics.CountAsync());
    }

    [TestMethod]
    public async Task GetByIdAsync_ShouldReturnTelemetryMetric()
    {
        var asset = await CreateAssetAsync();
        var metric = CreateMetric(asset.Id);

        await _repository.AddAsync(metric);

        var result = await _repository.GetByIdAsync(metric.Id);

        Assert.IsNotNull(result);
        Assert.AreEqual(metric.Value, result.Value);
    }

    [TestMethod]
    public async Task GetByIdAsync_ShouldReturnNull_WhenMetricDoesNotExist()
    {
        var result = await _repository.GetByIdAsync(999);

        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task GetByDeviceAssetIdAsync_ShouldReturnMatchingMetrics()
    {
        var asset = await CreateAssetAsync();

        await _repository.AddAsync(CreateMetric(asset.Id, 85));
        await _repository.AddAsync(CreateMetric(asset.Id, 90));

        var result = await _repository.GetByDeviceAssetIdAsync(asset.Id);

        Assert.HasCount(2, result);
    }

    [TestMethod]
    public async Task GetAllAsync_ShouldReturnAllTelemetryMetrics()
    {
        var asset = await CreateAssetAsync();

        await _repository.AddAsync(CreateMetric(asset.Id, 85));
        await _repository.AddAsync(CreateMetric(asset.Id, 90));

        var result = await _repository.GetAllAsync();

        Assert.HasCount(2, result);
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldRemoveTelemetryMetric()
    {
        var asset = await CreateAssetAsync();
        var metric = CreateMetric(asset.Id);

        await _repository.AddAsync(metric);
        await _repository.DeleteAsync(metric.Id);

        Assert.AreEqual(0, await _context.TelemetryMetrics.CountAsync());
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldNotFail_WhenMetricDoesNotExist()
    {
        await _repository.DeleteAsync(999);

        Assert.AreEqual(0, await _context.TelemetryMetrics.CountAsync());
    }
}