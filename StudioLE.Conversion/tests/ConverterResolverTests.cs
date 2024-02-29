using NUnit.Framework;
using StudioLE.Conversion.Tests.Resources;

namespace StudioLE.Conversion.Tests;

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
}
