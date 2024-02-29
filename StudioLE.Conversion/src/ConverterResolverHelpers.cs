using System.Reflection;

namespace StudioLE.Conversion;

/// <summary>
/// Methods to help with <see cref="ConverterResolver"/>.
/// </summary>
public static class ConverterResolverHelpers
{
    /// <summary>
    /// Resolve an converter instance that converts <paramref name="sourceType"/> to <paramref name="resultType"/>.
    /// </summary>
    /// <remarks>
    /// This method will searched the full inheritance hierarchy of <paramref name="sourceType"/> and <paramref name="resultType"/> to find a converter.
    /// The converter will be instantiated using <see cref="Activator.CreateInstance(Type)"/>.
    /// </remarks>
    /// <param name="resolver">The <see cref="ConverterResolver"/> to use.</param>
    /// <param name="sourceType">The <see cref="Type"/> to convert from.</param>
    /// <param name="resultType">The <see cref="Type"/> to convert to.</param>
    /// <returns>The converter <see cref="Type"/>, or null if no converter was found.</returns>
    public static object? ResolveActivated(this ConverterResolver resolver, Type sourceType, Type resultType)
    {
        Type? resolved = resolver.ResolveType(sourceType, resultType);
        return resolved is null
            ? null
            : Activate(resultType, resolved);
    }

    /// <summary>
    /// Try and convert <paramref name="source"/> to <paramref name="resultType"/> using the most appropriate converter.
    /// </summary>
    /// <param name="resolver">The <see cref="ConverterResolver"/> to use.</param>
    /// <param name="source">The instance to be converted.</param>
    /// <param name="resultType">The <see cref="Type"/> to convert to.</param>
    /// <returns><paramref name="source"/> converted to <paramref name="resultType"/>, or null if no converter was found.</returns>
    public static object? TryConvert(this ConverterResolver resolver, object source, Type resultType)
    {
        object? converter = resolver.ResolveActivated(source.GetType(), resultType);
        if (converter is null)
            return null;
        MethodInfo? method = converter.GetType().GetMethod("Convert");
        if (method is null)
            return null;
        return method.Invoke(converter, new[] { source });
    }

    /// <summary>
    /// Try and convert <paramref name="source"/> to <typeparamref name="TResult"/> using the most appropriate converter.
    /// </summary>
    /// <param name="resolver">The <see cref="ConverterResolver"/> to use.</param>
    /// <param name="source">The instance to be converted.</param>
    /// <typeparam name="TSource">The <see cref="Type"/> of <paramref name="source"/>.</typeparam>
    /// <typeparam name="TResult">The <see cref="Type"/> to convert to.</typeparam>
    /// <returns><paramref name="source"/> converted to <typeparamref name="TResult"/>, or null if no converter was found.</returns>
    public static TResult? TryConvert<TSource, TResult>(this ConverterResolver resolver, TSource source) where TResult : struct
    {
        object? converter = resolver.ResolveActivated(typeof(TSource), typeof(TResult));
        if (converter is null)
            return null;
        MethodInfo? method = converter.GetType().GetMethod("Convert");
        if (method is null)
            return null;
        object? result = method.Invoke(converter, new object[] { source! });
        return result is TResult tResult
            ? tResult
            : null;
    }

    private static object? Activate(Type resultType, Type converterType)
    {
        ConstructorInfo[] constructors = converterType.GetConstructors();
        ConstructorInfo? parameterlessConstructor = constructors
            .FirstOrDefault(x => x.GetParameters().Length == 0);
        if (parameterlessConstructor is not null)
            return Activator.CreateInstance(converterType);
        ConstructorInfo? typeConstructor = constructors
            .FirstOrDefault(x => x.GetParameters().Length == 1
                                 && x.GetParameters().First().ParameterType == typeof(Type));
        if (typeConstructor is not null)
            return Activator.CreateInstance(converterType, resultType);
        return null;
    }
}
