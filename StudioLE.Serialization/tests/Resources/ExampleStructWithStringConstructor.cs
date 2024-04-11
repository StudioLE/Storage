namespace StudioLE.Serialization.Tests.Resources;

public readonly struct ExampleStructWithStringConstructor
{
    private readonly int _value;

    public ExampleStructWithStringConstructor(string value)
    {
        _value = int.Parse(value);
    }

    public int GetValue()
    {
        return _value;
    }

    public override string ToString()
    {
        return _value.ToString();
    }
}
