using StudioLE.Patterns;

namespace StudioLE.Conversion;

/// <summary>
/// Build a <see cref="ConverterResolver"/> using a <see href="https://refactoring.guru/design-patterns/builder">builder pattern</see>.
/// </summary>
public class ConverterResolverBuilder : IBuilder<ConverterResolver>
{
    private readonly Dictionary<Type, Dictionary<Type, Type>> _registry = new();

    /// <summary>
    /// Register a converter, <typeparamref name="TConverter"/>, to convert <typeparamref name="TSource"/> to <typeparamref name="TResult"/>.
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
