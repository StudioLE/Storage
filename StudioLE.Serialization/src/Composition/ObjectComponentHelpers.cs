using System.Reflection;

namespace StudioLE.Serialization.Composition;

/// <summary>
/// Methods to help with <see cref="IObjectComponent"/>.
/// </summary>
public static class ObjectComponentHelpers
{
    /// <summary>
    /// Recursively get the <see cref="IObjectComponent.Children"/> of <paramref name="component"/> as a flattened enumerable.
    /// </summary>
    public static IEnumerable<IObjectComponent> Flatten(this IObjectComponent component)
    {
        return component
            .Children
            .SelectMany(property => Array.Empty<IObjectComponent>()
                .Append(property)
                .Concat(property.Flatten()));
    }

    /// <summary>
    /// Recursively get the <see cref="ObjectProperty"/> <see cref="IObjectComponent.Children"/>
    /// from <paramref name="component"/> as a flattened enumerable.
    /// </summary>
    public static IEnumerable<ObjectProperty> FlattenProperties(this IObjectComponent component)
    {
        return component.FlattenOfType<ObjectProperty>();
    }

    /// <summary>
    /// Recursively get the  <see cref="IObjectComponent.Children"/> of type <typeparamref name="T"/>
    /// from <paramref name="component"/> as a flattened enumerable.
    /// </summary>
    public static IEnumerable<T> FlattenOfType<T>(this IObjectComponent component) where T : IObjectComponent
    {
        return component
            .Children
            .OfType<T>()
            .SelectMany(tree => Array.Empty<T>()
                .Append(tree)
                .Concat(tree.FlattenOfType<T>()));
    }

    internal static IReadOnlyCollection<ObjectProperty> CreateProperties(IObjectComponent @this)
    {
        return @this
            .Type
            .GetProperties()
            .Where(IsSupported)
            .Select(property => new ObjectProperty(property, @this))
            .ToArray();
    }

    /// <summary>
    /// Is the property supported?
    /// </summary>
    private static bool IsSupported(PropertyInfo property)
    {
        return !property.IsIndexer();
    }

    /// <summary>
    /// Is the property an indexer?
    /// </summary>
    private static bool IsIndexer(this PropertyInfo @this)
    {
        return @this.GetIndexParameters().Length != 0;
    }
}
