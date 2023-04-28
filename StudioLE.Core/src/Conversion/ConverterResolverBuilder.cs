using StudioLE.Core.Patterns;

namespace StudioLE.Core.Conversion;

/// <summary>
/// Build an <see cref="ConverterResolver"/>.
/// </summary>
public class ConverterResolverBuilder : IBuilder<ConverterResolver>
{
    private readonly Dictionary<Type, Dictionary<Type, Type>> _registry = new();

    /// <summary>
    /// Register a converter as resolvable by <see cref="Type"/>.
    /// </summary>
    /// <returns>The builder.</returns>
    public ConverterResolverBuilder Register<TSource, TResult, TConverter>() where TConverter : IConverter<TSource, TResult>
    {

        Type sourceType = Nullable.GetUnderlyingType(typeof(TSource)) ?? typeof(TSource);
        Type resultType = Nullable.GetUnderlyingType(typeof(TResult)) ?? typeof(TResult);
        Type converterType = typeof(TConverter);
        if (!_registry.ContainsKey(sourceType))
            _registry.Add(sourceType, new());
        _registry[sourceType].Add(resultType, converterType);
        return this;
    }

    /// <inheritdoc/>
    public ConverterResolver Build()
    {
        return new(_registry);
    }
}
