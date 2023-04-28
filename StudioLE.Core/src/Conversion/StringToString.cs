namespace StudioLE.Core.Conversion;

/// <inheritdoc />
public class StringToString : IConverter<string, string?>
{
    /// <inheritdoc />
    public string Convert(string source)
    {
        return source;
    }
}
