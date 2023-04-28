using NUnit.Framework;
using StudioLE.Core.Conversion;
using StudioLE.Core.Tests.Resources;

namespace StudioLE.Core.Tests.Converters;

internal sealed class StringToTests
{
    [TestCase("Default", ExampleEnum.Default)]
    [TestCase("Alternative", ExampleEnum.Alternative)]
    [TestCase("0", ExampleEnum.Default)]
    [TestCase("1", ExampleEnum.Alternative)]
    [TestCase("Invalid", null)]
    [TestCase("default", null)]
    [TestCase("", null)]
    [TestCase("10", (ExampleEnum)10)]
    public void StringToEnum_Convert(string source, ExampleEnum? expected)
    {
        // Arrange
        StringToEnum<ExampleEnum> converter = new();

        // Act
        ExampleEnum? result = converter.Convert(source);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}
