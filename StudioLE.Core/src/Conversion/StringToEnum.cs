namespace StudioLE.Core.Conversion;

#if ! NETSTANDARD2_0
/// <inheritdoc />
public class StringToEnum : IConverter<string, Enum?>
{
    private readonly Type _enumType;

    public StringToEnum(Type enumType)
    {
        _enumType = enumType;
    }

    /// <inheritdoc />
    public Enum? Convert(string source)
    {
        return Enum.TryParse(_enumType, source, out object? result)
            ? (Enum)result
            : null;
    }
}
#endif

/// <inheritdoc />
public class StringToEnum<TEnum> : IConverter<string, TEnum?> where TEnum : struct, Enum
{
    /// <inheritdoc />
    public TEnum? Convert(string source)
    {
        return Enum.TryParse(source, out TEnum result)
            ? result
            : null;
    }
}
