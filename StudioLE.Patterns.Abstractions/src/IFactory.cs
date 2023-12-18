namespace StudioLE.Patterns;

/// <summary>
/// Create a <typeparamref name="TResult"/> from a <typeparamref name="TSource"/>
/// using a <see href="https://refactoring.guru/design-patterns/factory-method">factory method pattern</see>.
/// </summary>
public interface IFactory<in TSource, out TResult>
{
    /// <summary>
    /// Create a <typeparamref name="TResult"/> from a <typeparamref name="TSource"/>.
    /// </summary>
    /// <param name="source">The object to create from.</param>
    /// <returns>The created <typeparamref name="TResult"/>.</returns>
    public TResult Create(TSource source);
}
