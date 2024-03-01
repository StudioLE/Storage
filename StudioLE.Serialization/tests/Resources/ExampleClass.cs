using System.ComponentModel.DataAnnotations;

namespace StudioLE.Serialization.Tests.Resources;

public class ExampleClass
{
    [Required]
    public string StringValue { get; set; } = string.Empty;

    [Required]
    [Range(10, 20)]
    public int IntegerValue { get; set; }

    [Required]
    [Range(0, 1)]
    public double DoubleValue { get; set; }

    [Required]
    public bool BooleanValue { get; set; }

    [Required]
    public string Arg1Value { get; set; } = string.Empty;

    [ValidateComplexType]
    public ExampleNestedClass Nested { get; set; } = new();

    [ValidateComplexType]
    public ExampleRecordStruct RecordStruct { get; set; } = new();

    [ValidateComplexType]
    public ExampleRecordStructWithStringConstructor RecordStructByConstructor { get; set; } = new();
}
