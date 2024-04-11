using NUnit.Framework;
using StudioLE.Diagnostics;
using StudioLE.Diagnostics.NUnit;
using StudioLE.Extensions.System;
using StudioLE.Serialization.Composition;
using StudioLE.Serialization.Tests.Resources;
using StudioLE.Verify;

namespace StudioLE.Serialization.Tests.Composition;

internal sealed class ObjectTreeTests
{
    private readonly IContext _context = new NUnitContext();

    [Test]
    public async Task ObjectTree_FlattenProperties()
    {
        // Arrange
        ExampleClass inputs = new();
        ObjectTree objectTree = new(inputs);

        // Act
        ObjectProperty[] properties = objectTree
            .FlattenProperties()
            .ToArray();
        string[] output = properties
            .Select(x => $"{x.FullKey}: {x.Type}")
            .ToArray();

        // Assert
        await _context.Verify(output.Join());
    }

    [Test]
    public async Task ObjectTree_ValidateValue()
    {
        // Arrange
        ExampleClass inputs = new();
        ObjectTree objectTree = new(inputs);

        // Act
        ObjectProperty[] properties = objectTree
            .FlattenProperties()
            .ToArray();
        string[] errors = properties
            .SelectMany(x => x.ValidateValue())
            .ToArray();

        // Assert
        await _context.Verify(errors.Join());
    }

    [TestCase("Hello, world!")]
    public async Task ObjectTree_SupportedTypes(object obj)
    {
        // Arrange
        ObjectTree objectTree = new(obj);

        // Act
        ObjectProperty[] properties = objectTree
            .FlattenProperties()
            .ToArray();
        string[] output = properties
            .Select(x => $"{x.Type} {x.FullKey}: {x.GetValue()}")
            .ToArray();

        // Assert
        await _context.Verify(output.Join());
    }

    [Test]
    public async Task ObjectTreeProperty_SetValue_RecordStruct()
    {
        // Arrange
        ExampleRecordStruct obj = new()
        {
            RecordStructStringValue = "This is a string value.",
            RecordStructArgValue = "This is an argument value."
        };
        ObjectTree objectTree = new(obj);

        // Act
        objectTree.Children.ElementAt(0).SetValue("This is a new string value.");
        ObjectProperty[] properties = objectTree
            .FlattenProperties()
            .ToArray();
        string[] output = properties
            .Select(x => $"{x.Type} {x.FullKey}: {x.GetValue()}")
            .ToArray();

        // Assert
        await _context.Verify(output.Join());
    }

    [Test]
    public async Task ObjectTreeProperty_SetValue_NestedRecordStruct()
    {
        // Arrange
        ExampleClass inputs = new();
        ObjectTree objectTree = new(inputs);
        IObjectComponent recordProperty = objectTree
            .Children
            .First(x => x.Type == typeof(ExampleRecordStruct));

        // Act
        recordProperty.Children.ElementAt(0).SetValue("This is a new string value.");
        ObjectProperty[] properties = objectTree
            .FlattenProperties()
            .ToArray();
        string[] output = properties
            .Select(x => $"{x.Type} {x.FullKey}: {x.GetValue()}")
            .ToArray();

        // Assert
        await _context.Verify(output.Join());
    }

    [Test]
    public async Task ObjectTreeProperty_SetValue_NestedNestedRecordStruct()
    {
        // Arrange
        ExampleClass inputs = new();
        ObjectTree objectTree = new(inputs);
        IObjectComponent recordProperty = objectTree
            .Children
            .First(x => x.Type == typeof(ExampleRecordStruct));
        IObjectComponent nestedRecordProperty = recordProperty
            .Children
            .First(x => x.Type == typeof(ExampleNestedRecordStruct));

        // Act
        nestedRecordProperty.Children.ElementAt(0).SetValue(9);
        ObjectProperty[] properties = objectTree
            .FlattenProperties()
            .ToArray();
        string[] output = properties
            .Select(x => $"{x.Type} {x.FullKey}: {x.GetValue()}")
            .ToArray();

        // Assert
        await _context.Verify(output.Join());
    }

    [Test]
    [Ignore("Redundant")]
    public async Task ObjectTreeProperty_SetValue_StructWithStringConstructor()
    {
        // Arrange
        ExampleClass inputs = new();
        ObjectTree objectTree = new(inputs);
        IObjectComponent objectComponent = objectTree
            .Children
            .First(x => x.Type == typeof(ExampleStructWithStringConstructor));

        // TODO: Use parser here to convert value

        // Act
        objectComponent.SetValue("This is a new string value.");
        ObjectProperty[] properties = objectTree
            .FlattenProperties()
            .ToArray();
        string[] output = properties
            .Select(x => $"{x.Type} {x.FullKey}: {x.GetValue()}")
            .ToArray();

        // Assert
        await _context.Verify(output.Join());
    }
}
