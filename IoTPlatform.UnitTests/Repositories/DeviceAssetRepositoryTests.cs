using IoTPlatform.Domain.Entities;
using IoTPlatform.Domain.ValueObjects;
using IoTPlatform.Infrastructure.Data;
using IoTPlatform.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IoTPlatform.UnitTests.Repositories;

[TestClass]
public class DeviceAssetRepositoryTests
{
    private IoTPlatformDbContext _context = null!;
    private DeviceAssetRepository _repository = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<IoTPlatformDbContext>()
            .UseSqlite("Data Source=:memory:")
            .Options;

        _context = new IoTPlatformDbContext(options);
        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();

        _repository = new DeviceAssetRepository(_context);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.CloseConnection();
        _context.Dispose();
    }

    private static DeviceAsset CreateAsset(
        string serialNumber = "ENG-12345")
    {
        return new DeviceAsset(
            new SerialNumber(serialNumber),
            "Test Engine",
            "Engine",
            new OperatingRange(20, 120));
    }

    [TestMethod]
    public async Task AddAsync_ShouldSaveDeviceAsset()
    {
        var asset = CreateAsset();

        await _repository.AddAsync(asset);

        Assert.AreEqual(1, await _context.DeviceAssets.CountAsync());
    }

    [TestMethod]
    public async Task GetByIdAsync_ShouldReturnDeviceAsset()
    {
        var asset = CreateAsset();
        await _repository.AddAsync(asset);

        var result = await _repository.GetByIdAsync(asset.Id);

        Assert.IsNotNull(result);
        Assert.AreEqual(asset.SerialNumber.Value, result.SerialNumber.Value);
    }

    [TestMethod]
    public async Task GetByIdAsync_ShouldReturnNull_WhenAssetDoesNotExist()
    {
        var result = await _repository.GetByIdAsync(999);

        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task GetBySerialNumberAsync_ShouldReturnDeviceAsset()
    {
        var asset = CreateAsset();
        await _repository.AddAsync(asset);

        var result = await _repository.GetBySerialNumberAsync("ENG-12345");

        Assert.IsNotNull(result);
        Assert.AreEqual(asset.Id, result.Id);
    }

    [TestMethod]
    public async Task GetAllAsync_ShouldReturnAllDeviceAssets()
    {
        await _repository.AddAsync(CreateAsset("ENG-12345"));
        await _repository.AddAsync(CreateAsset("ENG-12346"));

        var result = await _repository.GetAllAsync();

        Assert.HasCount(2, result);
    }

    [TestMethod]
    public async Task UpdateAsync_ShouldSaveChangedDeviceAssetStatus()
    {
        var asset = CreateAsset();
        await _repository.AddAsync(asset);

        asset.ChangeStatus(DeviceStatus.UnderMaintenance);
        await _repository.UpdateAsync(asset);

        var result = await _repository.GetByIdAsync(asset.Id);

        Assert.IsNotNull(result);
        Assert.AreEqual(DeviceStatus.UnderMaintenance, result.Status);
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldRemoveDeviceAsset()
    {
        var asset = CreateAsset();
        await _repository.AddAsync(asset);

        await _repository.DeleteAsync(asset.Id);

        Assert.AreEqual(0, await _context.DeviceAssets.CountAsync());
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldNotFail_WhenAssetDoesNotExist()
    {
        await _repository.DeleteAsync(999);

        Assert.AreEqual(0, await _context.DeviceAssets.CountAsync());
    }
}