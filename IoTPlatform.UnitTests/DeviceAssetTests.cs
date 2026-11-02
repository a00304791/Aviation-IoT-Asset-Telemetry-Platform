using Microsoft.VisualStudio.TestTools.UnitTesting;
using IoTPlatform.Domain.Entities;
using IoTPlatform.Domain.ValueObjects;
using IoTPlatform.Domain.Exceptions;

namespace IoTPlatform.UnitTests;

[TestClass]
public class DeviceAssetTests
{
    private static DeviceAsset CreateAsset()
    {
        return new DeviceAsset(
            new SerialNumber("ENG-12345"),
            "Engine Temperature Sensor",
            "Temperature Sensor",
            new OperatingRange(20, 200));
    }

    [TestMethod]
    public void Constructor_ValidDetails_CreatesActiveAsset()
    {
        var asset = CreateAsset();

        Assert.AreEqual(DeviceStatus.Active, asset.Status);
        Assert.AreEqual("ENG-12345", asset.SerialNumber.Value);
    }

    [TestMethod]
    public void ChangeStatus_ActiveToUnderMaintenance_Succeeds()
    {
        var asset = CreateAsset();

        asset.ChangeStatus(DeviceStatus.UnderMaintenance);

        Assert.AreEqual(DeviceStatus.UnderMaintenance, asset.Status);
    }

    [TestMethod]
    public void ChangeStatus_ActiveToRetired_Succeeds()
    {
        var asset = CreateAsset();

        asset.ChangeStatus(DeviceStatus.Retired);

        Assert.AreEqual(DeviceStatus.Retired, asset.Status);
    }

    [TestMethod]
    public void ChangeStatus_UnderMaintenanceToActive_Succeeds()
    {
        var asset = CreateAsset();
        asset.ChangeStatus(DeviceStatus.UnderMaintenance);

        asset.ChangeStatus(DeviceStatus.Active);

        Assert.AreEqual(DeviceStatus.Active, asset.Status);
    }

    [TestMethod]
    public void ChangeStatus_UnderMaintenanceToRetired_Succeeds()
    {
        var asset = CreateAsset();
        asset.ChangeStatus(DeviceStatus.UnderMaintenance);

        asset.ChangeStatus(DeviceStatus.Retired);

        Assert.AreEqual(DeviceStatus.Retired, asset.Status);
    }

    [TestMethod]
    public void ChangeStatus_RetiredToActive_ThrowsException()
    {
        var asset = CreateAsset();
        asset.ChangeStatus(DeviceStatus.Retired);

        Assert.ThrowsExactly<InvalidStatusTransitionException>(
            () => asset.ChangeStatus(DeviceStatus.Active));
    }

    [TestMethod]
    public void ChangeStatus_RetiredToUnderMaintenance_ThrowsException()
    {
        var asset = CreateAsset();
        asset.ChangeStatus(DeviceStatus.Retired);

        Assert.ThrowsExactly<InvalidStatusTransitionException>(
            () => asset.ChangeStatus(DeviceStatus.UnderMaintenance));
    }

    [TestMethod]
    public void ChangeStatus_SameStatus_ThrowsException()
    {
        var asset = CreateAsset();

        Assert.ThrowsExactly<InvalidStatusTransitionException>(
            () => asset.ChangeStatus(DeviceStatus.Active));
    }
}