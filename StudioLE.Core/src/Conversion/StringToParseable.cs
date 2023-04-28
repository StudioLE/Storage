#if NET7_0_OR_GREATER
namespace StudioLE.Core.Conversion;

/// <inheritdoc/>
public class StringToParseable<TStruct> : IConverter<string, TStruct?> where TStruct : struct, IParsable<TStruct>
{
    private readonly IFormatProvider? _formatProvider;

    public StringToParseable(IFormatProvider? formatProvider)
    {
        _formatProvider = formatProvider;
    }

    /// <inheritdoc/>
    public TStruct? Convert(string source)
    {
        return TStruct.TryParse(source, _formatProvider, out TStruct result)
            ? result
            : null;
    }
}
#endif
