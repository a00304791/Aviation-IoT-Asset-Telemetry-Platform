using Microsoft.VisualStudio.TestTools.UnitTesting;
using IoTPlatform.Domain.ValueObjects;
using IoTPlatform.Domain.Exceptions;

namespace IoTPlatform.UnitTests;

[TestClass]
public class SerialNumberTests
{
    [TestMethod]
    public void Constructor_ValidSerialNumber_CreatesSerialNumber()
    {
        // Arrange
        string value = "ENG-12345";

        // Act
        var serialNumber = new SerialNumber(value);

        // Assert
        Assert.AreEqual(value, serialNumber.Value);
    }

    [TestMethod]
    public void Constructor_LowercaseLetters_ThrowsException()
    {
        Assert.ThrowsExactly<InvalidSerialNumberException>(
            () => new SerialNumber("eng-12345"));
    }

    [TestMethod]
    public void Constructor_MissingHyphen_ThrowsException()
    {
        Assert.ThrowsExactly<InvalidSerialNumberException>(
            () => new SerialNumber("ENG12345"));
    }

    [TestMethod]
    public void Constructor_TooFewLetters_ThrowsException()
    {
        Assert.ThrowsExactly<InvalidSerialNumberException>(
            () => new SerialNumber("EN-12345"));
    }

    [TestMethod]
    public void Constructor_TooManyDigits_ThrowsException()
    {
        Assert.ThrowsExactly<InvalidSerialNumberException>(
            () => new SerialNumber("ENG-123456"));
    }

    [TestMethod]
    public void Constructor_EmptyValue_ThrowsException()
    {
        Assert.ThrowsExactly<InvalidSerialNumberException>(
            () => new SerialNumber(""));
    }

    [TestMethod]
    public void Constructor_NullValue_ThrowsException()
    {
        Assert.ThrowsExactly<InvalidSerialNumberException>(
            () => new SerialNumber(null!));
    }
}