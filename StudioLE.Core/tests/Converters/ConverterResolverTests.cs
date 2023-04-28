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
}
