using Microsoft.VisualStudio.TestTools.UnitTesting;
using IoTPlatform.Domain.Entities;
using IoTPlatform.Domain.ValueObjects;

namespace IoTPlatform.UnitTests;

[TestClass]
public class AlertRuleTests
{
    private static AlertRule CreateRule()
    {
        return new AlertRule(
            1,
            "Engine Temperature Alert",
            TelemetryMetricType.Temperature,
            new OperatingRange(20, 200));
    }

    [TestMethod]
    public void Constructor_ValidDetails_CreatesEnabledRule()
    {
        var rule = CreateRule();

        Assert.IsTrue(rule.IsEnabled);
        Assert.AreEqual("Engine Temperature Alert", rule.Name);
    }

    [TestMethod]
    public void IsOutsideThreshold_ValueWithinRange_ReturnsFalse()
    {
        var rule = CreateRule();

        Assert.IsFalse(rule.IsOutsideThreshold(100));
    }

    [TestMethod]
    public void IsOutsideThreshold_ValueBelowRange_ReturnsTrue()
    {
        var rule = CreateRule();

        Assert.IsTrue(rule.IsOutsideThreshold(10));
    }

    [TestMethod]
    public void IsOutsideThreshold_ValueAboveRange_ReturnsTrue()
    {
        var rule = CreateRule();

        Assert.IsTrue(rule.IsOutsideThreshold(210));
    }

    [TestMethod]
    public void IsOutsideThreshold_ValueAtBoundary_ReturnsFalse()
    {
        var rule = CreateRule();

        Assert.IsFalse(rule.IsOutsideThreshold(20));
        Assert.IsFalse(rule.IsOutsideThreshold(200));
    }

    [TestMethod]
    public void Disable_EnabledRule_DisablesRule()
    {
        var rule = CreateRule();

        rule.Disable();

        Assert.IsFalse(rule.IsEnabled);
    }

    [TestMethod]
    public void Enable_DisabledRule_EnablesRule()
    {
        var rule = CreateRule();
        rule.Disable();

        rule.Enable();

        Assert.IsTrue(rule.IsEnabled);
    }
}