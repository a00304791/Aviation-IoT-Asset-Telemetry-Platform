using IoTPlatform.Domain.Entities;

namespace IoTPlatform.UnitTests;

[TestClass]
public class MaintenanceLogTests
{
    private static MaintenanceLog CreateLog()
    {
        return new MaintenanceLog(
            1,
            "Inspection",
            "Engine temperature sensor inspection",
            DateTime.UtcNow,
            "John Smith");
    }

    [TestMethod]
    public void Constructor_ValidDetails_CreatesMaintenanceLog()
    {
        var log = CreateLog();

        Assert.AreEqual(1, log.DeviceAssetId);
        Assert.AreEqual("Inspection", log.MaintenanceType);
        Assert.AreEqual("John Smith", log.TechnicianName);
    }

    [TestMethod]
    public void Constructor_ZeroAssetId_ThrowsException()
    {
        Assert.ThrowsExactly<ArgumentException>(
            () => new MaintenanceLog(
                0, "Inspection", "Sensor inspection",
                DateTime.UtcNow, "John Smith"));
    }

    [TestMethod]
    public void Constructor_NegativeAssetId_ThrowsException()
    {
        Assert.ThrowsExactly<ArgumentException>(
            () => new MaintenanceLog(
                -1, "Inspection", "Sensor inspection",
                DateTime.UtcNow, "John Smith"));
    }

    [TestMethod]
    public void Constructor_EmptyMaintenanceType_ThrowsException()
    {
        Assert.ThrowsExactly<ArgumentException>(
            () => new MaintenanceLog(
                1, "", "Sensor inspection",
                DateTime.UtcNow, "John Smith"));
    }

    [TestMethod]
    public void Constructor_EmptyDescription_ThrowsException()
    {
        Assert.ThrowsExactly<ArgumentException>(
            () => new MaintenanceLog(
                1, "Inspection", "",
                DateTime.UtcNow, "John Smith"));
    }

    [TestMethod]
    public void Constructor_EmptyTechnicianName_ThrowsException()
    {
        Assert.ThrowsExactly<ArgumentException>(
            () => new MaintenanceLog(
                1, "Inspection", "Sensor inspection",
                DateTime.UtcNow, ""));
    }
}