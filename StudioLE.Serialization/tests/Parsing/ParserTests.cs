using NUnit.Framework;
using StudioLE.Serialization.Parsing;
using StudioLE.Serialization.Tests.Resources;

namespace StudioLE.Serialization.Tests.Parsing;

internal sealed class ParserTests
{
    private const byte PiByte = 31;
    private const int PiInt = 314159265;
    private const uint PiUint = 3141592654;
    private const long PiLong = 3141592653589793238;
    private const float PiFloat = 3.14159265f;
    private const double PiDouble = 3.1415926535897d;
    private const decimal PiDecimal = 3.14159265358979323846264338m;

    private readonly IParser _parser = new Parser();

    [TestCase("31", typeof(byte), PiByte)]
    [TestCase("314159265", typeof(int), PiInt)]
    [TestCase("3141592654", typeof(uint), PiUint)]
    [TestCase("3141592653589793238", typeof(long), PiLong)]
    [TestCase("3.14159265", typeof(float), PiFloat)]
    [TestCase("3.1415926535897", typeof(double), PiDouble)]
    // Attributes don't support decimals as they aren't a primitive
    // [TestCase("3.14159265358979323846264338", typeof(decimal), PiDecimal)]
    [TestCase("true", typeof(bool), true)]
    [TestCase("false", typeof(bool), false)]
    [TestCase("FALSE", typeof(bool), false)]
    [TestCase("Three", typeof(ExampleEnum), ExampleEnum.Three)]
    public void Parser_Parse(string source, Type target, object expected)
    {
        // Arrange
        // Act
        object? parsed = _parser.Parse(source, target);

        // Assert
        Assert.That(parsed, Is.EqualTo(expected));
        Assert.That(parsed, Is.TypeOf(target));
        Assert.That(parsed, Is.Not.Null);
    }

    [TestCase("3.147", typeof(int))]
    [TestCase("yes", typeof(bool))]
    [TestCase("y", typeof(bool))]
    [TestCase("Ten", typeof(ExampleEnum))]
    public void Parser_Parse_Invalid(string source, Type target)
    {
        // Arrange
        // Act
        object? parsed = _parser.Parse(source, target);

        // Assert
        Assert.That(parsed, Is.Null);
    }

    [Test]
    public void Parser_Parse_StringConstructor()
    {
        // Arrange
        const int source = 12345;
        Type target = typeof(ExampleStructWithStringConstructor);
        ExampleStructWithStringConstructor expected = new(source.ToString());

        // Act
        object? parsed = _parser.Parse(source.ToString(), target);

        // Assert
        Assert.That(parsed, Is.EqualTo(expected));
        Assert.That(parsed, Is.TypeOf(target));
        Assert.That(parsed, Is.Not.Null);
        if(parsed is ExampleStructWithStringConstructor actual)
            Assert.That(actual.GetValue(), Is.EqualTo(source));
    }

    [Test]
    public void Parser_CanParse_StringConstructor()
    {
        // Arrange
        Type target = typeof(ExampleStructWithStringConstructor);

        // Act
        bool isParseable = _parser.CanParse(target);

        // Assert
        Assert.That(isParseable);
    }
}
