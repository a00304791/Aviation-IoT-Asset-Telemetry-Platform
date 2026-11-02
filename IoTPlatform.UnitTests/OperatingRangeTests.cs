using Microsoft.VisualStudio.TestTools.UnitTesting;
using IoTPlatform.Domain.ValueObjects;
using IoTPlatform.Domain.Exceptions;

namespace IoTPlatform.UnitTests;

[TestClass]
public class OperatingRangeTests
{
    [TestMethod]
    public void Constructor_ValidRange_CreatesOperatingRange()
    {
        var range = new OperatingRange(20, 200);

        Assert.AreEqual(20m, range.Minimum);
        Assert.AreEqual(200m, range.Maximum);
    }

    [TestMethod]
    public void Constructor_ZeroMinimum_ThrowsException()
    {
        Assert.ThrowsExactly<InvalidOperatingRangeException>(
            () => new OperatingRange(0, 200));
    }

    [TestMethod]
    public void Constructor_NegativeMinimum_ThrowsException()
    {
        Assert.ThrowsExactly<InvalidOperatingRangeException>(
            () => new OperatingRange(-10, 200));
    }

    [TestMethod]
    public void Constructor_MaximumNotGreaterThanMinimum_ThrowsException()
    {
        Assert.ThrowsExactly<InvalidOperatingRangeException>(
            () => new OperatingRange(100, 100));
    }

    [TestMethod]
    public void Contains_ValueWithinRange_ReturnsTrue()
    {
        var range = new OperatingRange(20, 200);

        Assert.IsTrue(range.Contains(100));
    }

    [TestMethod]
    public void Contains_ValueBelowRange_ReturnsFalse()
    {
        var range = new OperatingRange(20, 200);

        Assert.IsFalse(range.Contains(10));
    }

    [TestMethod]
    public void Contains_ValueAboveRange_ReturnsFalse()
    {
        var range = new OperatingRange(20, 200);

        Assert.IsFalse(range.Contains(210));
    }

    [TestMethod]
    public void Contains_ValueAtBoundary_ReturnsTrue()
    {
        var range = new OperatingRange(20, 200);

        Assert.IsTrue(range.Contains(20));
        Assert.IsTrue(range.Contains(200));
    }
}