using System.Reflection;

namespace StudioLE.Core.Conversion;

/// <summary>
/// A dependency injection service to resolve a converter for a specified type.
/// </summary>
/// <remarks>
/// The resolver should be constructed via a <see cref="ConverterResolverBuilder"/>
/// and registered as a dependency injection service.
/// </remarks>
public class ConverterResolver
{
    private readonly Dictionary<Type, Dictionary<Type, Type>> _registry;

    /// <summary>
    /// Construct an <see cref="ConverterResolver"/> from an existing dictionary.
    /// </summary>
    /// <remarks>
    /// This constructor is used by <see cref="ConverterResolverBuilder"/>.
    /// </remarks>
    /// <param name="registry">The assemblies.</param>
    internal ConverterResolver(Dictionary<Type, Dictionary<Type, Type>> registry)
    {
        _registry = registry;
    }

    /// <summary>
    /// Resolve a converter <see cref="Type"/> that converts <paramref name="sourceType"/> to <paramref name="resultType"/>.
    /// </summary>
    public Type? ResolveTypeExact(Type sourceType, Type resultType)
    {
        if (!_registry.ContainsKey(sourceType))
            return null;
        if (!_registry[sourceType].ContainsKey(resultType))
            return null;
        return _registry[sourceType][resultType];
    }

    /// <summary>
    /// Resolve a converter <see cref="Type"/> that converts <paramref name="sourceType"/> to <paramref name="resultType"/>.
    /// </summary>
    public Type? ResolveType(Type sourceType, Type resultType)
    {
        IReadOnlyCollection<Type> sourceTypes = GetTypeHierarchy(sourceType);
        IReadOnlyCollection<Type> resultTypes = GetTypeHierarchy(resultType);
        return sourceTypes.Select(source => resultTypes
                .Select(result => ResolveTypeExact(source, result))
                .OfType<Type>()
                .FirstOrDefault())
            .FirstOrDefault();
    }

    /// <summary>
    /// Resolve a converter <see cref="Type"/> that converts <paramref name="sourceType"/> to <paramref name="resultType"/>.
    /// </summary>
    public object? ResolveActivated(Type sourceType, Type resultType)
    {
        Type? resolved = ResolveType(sourceType, resultType);
        return resolved is null
            ? null
            : Activate(resultType, resolved);
    }

    public object? TryConvert(object source, Type resultType)
    {
        object? converter = ResolveActivated(source.GetType(), resultType);
        if (converter is null)
            return null;
        MethodInfo? method = converter.GetType().GetMethod("Convert");
        if (method is null)
            return null;
        return method.Invoke(converter, new[]{ source });
    }

    public TResult? TryConvert<TSource, TResult>(TSource source) where TResult : struct
    {
        object? converter = ResolveActivated(typeof(TSource), typeof(TResult));
        if (converter is null)
            return null;
        MethodInfo? method = converter.GetType().GetMethod("Convert");
        if (method is null)
            return null;
        object? result = method.Invoke(converter, new object[]{ source! });
        return result is TResult tResult
            ? tResult
            : null;
    }

    private static object? Activate(Type resultType, Type converterType)
    {
        ConstructorInfo[] constructors = converterType.GetConstructors();
        ConstructorInfo? parameterlessConstructor = constructors
            .FirstOrDefault(x => x.GetParameters().Length == 0);
        if(parameterlessConstructor is not null)
            return Activator.CreateInstance(converterType);
        ConstructorInfo? typeConstructor = constructors
            .FirstOrDefault(x => x.GetParameters().Length == 1
            && x.GetParameters().First().ParameterType == typeof(Type));
        if(typeConstructor is not null)
            return Activator.CreateInstance(converterType, resultType);
        return null;
    }


    private static IReadOnlyCollection<Type> GetTypeHierarchy(Type type)
    {
        List<Type> hierarchy = new();
        while (true)
        {
            hierarchy.Add(type);
            if (type.BaseType is null)
                break;
            type = type.BaseType;
        }
        return hierarchy;
    }

    public static ConverterResolver Default()
    {
        return new ConverterResolverBuilder()
            #if ! NETSTANDARD2_0
            .Register<string, Enum?, StringToEnum>()
            #endif
            .Register<string, int?, StringToInteger>()
            .Register<string, double?, StringToDouble>()
            .Register<string, string?, StringToString>()
            .Build();
    }
}
