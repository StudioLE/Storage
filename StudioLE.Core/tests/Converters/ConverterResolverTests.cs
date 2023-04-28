using NUnit.Framework;
using StudioLE.Core.Conversion;
using StudioLE.Core.Tests.Resources;

namespace StudioLE.Core.Tests.Converters;

internal sealed class ConverterResolverTests
{
    [TestCase(typeof(ExampleEnum), typeof(StringToEnum))]
    [TestCase(typeof(double), typeof(StringToDouble))]
    [TestCase(typeof(int), typeof(StringToInteger))]
    [TestCase(typeof(string), typeof(StringToString))]
    public void ConverterResolver_ResolveType(Type resultType, Type expectedConverterType)
    {
        // Arrange
        ConverterResolver resolver = ConverterResolver.Default();

        // Act
        Type? converter = resolver.ResolveType(typeof(string), resultType);

        // Assert
        Assert.That(converter, Is.Not.Null);
        Assert.That(converter, Is.EqualTo(expectedConverterType));
    }

    [TestCase(typeof(ExampleEnum), typeof(StringToEnum))]
    [TestCase(typeof(double), typeof(StringToDouble))]
    [TestCase(typeof(int), typeof(StringToInteger))]
    [TestCase(typeof(string), typeof(StringToString))]
    public void ConverterResolver_ResolveActivated(Type resultType, Type expectedConverterType)
    {
        // Arrange
        ConverterResolver resolver = ConverterResolver.Default();

        // Act
        object? converter = resolver.ResolveActivated(typeof(string), resultType);

        // Assert
        Assert.That(converter, Is.Not.Null);
        Assert.That(converter?.GetType(), Is.EqualTo(expectedConverterType));
    }

    [TestCase("Default", ExampleEnum.Default)]
    [TestCase("Alternative", ExampleEnum.Alternative)]
    [TestCase("INVALID", null)]
    [TestCase("10", (ExampleEnum)10)]
    public void ConverterResolver_ResolveActivated_Enum(string value, ExampleEnum? expected)
    {
        // Arrange
        ConverterResolver resolver = ConverterResolver.Default();
        object? converter = resolver.ResolveActivated(typeof(string), typeof(ExampleEnum));
        if (converter is not StringToEnum stringToEnum)
            throw new("Resolved incorrect type");

        // Act
        Enum? result = stringToEnum.Convert(value);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("1", 1)]
    [TestCase("1.01", 1.01)]
    [TestCase("-1.01", -1.01)]
    [TestCase("", null)]
    [TestCase(null, null)]
    public void ConverterResolver_TryConvert_Double(string value, double? expected)
    {
        // Arrange
        ConverterResolver resolver = ConverterResolver.Default();

        // Act
        double? result = resolver.TryConvert<string, double>(value);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("Default", ExampleEnum.Default)]
    [TestCase("Alternative", ExampleEnum.Alternative)]
    [TestCase("INVALID", null)]
    [TestCase("10", (ExampleEnum)10)]
    public void ConverterResolver_TryConvert_Enum(string value, ExampleEnum? expected)
    {
        // Arrange
        ConverterResolver resolver = ConverterResolver.Default();

        // Act
        ExampleEnum? result = resolver.TryConvert<string, ExampleEnum>(value);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}
