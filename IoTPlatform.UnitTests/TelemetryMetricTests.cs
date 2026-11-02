using Microsoft.VisualStudio.TestTools.UnitTesting;
using IoTPlatform.Domain.Entities;

namespace IoTPlatform.UnitTests;

[TestClass]
public class TelemetryMetricTests
{
    private static TelemetryMetric CreateMetric()
    {
        return new TelemetryMetric(
            1,
            TelemetryMetricType.Temperature,
            85.5m,
            "Celsius",
            DateTime.UtcNow);
    }

    [TestMethod]
    public void Constructor_ValidDetails_CreatesMetric()
    {
        var metric = CreateMetric();

        Assert.AreEqual(1, metric.DeviceAssetId);
        Assert.AreEqual(TelemetryMetricType.Temperature, metric.MetricType);
        Assert.AreEqual(85.5m, metric.Value);
        Assert.AreEqual("Celsius", metric.Unit);
    }

    [TestMethod]
    public void Constructor_ZeroAssetId_ThrowsException()
    {
        Assert.ThrowsExactly<ArgumentException>(
            () => new TelemetryMetric(
                0, TelemetryMetricType.Temperature, 85.5m, "Celsius", DateTime.UtcNow));
    }

    [TestMethod]
    public void Constructor_NegativeAssetId_ThrowsException()
    {
        Assert.ThrowsExactly<ArgumentException>(
            () => new TelemetryMetric(
                -1, TelemetryMetricType.Temperature, 85.5m, "Celsius", DateTime.UtcNow));
    }

    [TestMethod]
    public void Constructor_InvalidMetricType_ThrowsException()
    {
        Assert.ThrowsExactly<ArgumentException>(
            () => new TelemetryMetric(
                1, (TelemetryMetricType)999, 85.5m, "Celsius", DateTime.UtcNow));
    }

    [TestMethod]
    public void Constructor_EmptyUnit_ThrowsException()
    {
        Assert.ThrowsExactly<ArgumentException>(
            () => new TelemetryMetric(
                1, TelemetryMetricType.Temperature, 85.5m, "", DateTime.UtcNow));
    }

    [TestMethod]
    public void Constructor_ValidVibrationMetric_CreatesMetric()
    {
        var metric = new TelemetryMetric(
            2,
            TelemetryMetricType.Vibration,
            4.2m,
            "mm/s",
            DateTime.UtcNow);

        Assert.AreEqual(TelemetryMetricType.Vibration, metric.MetricType);
        Assert.AreEqual(4.2m, metric.Value);
    }
}