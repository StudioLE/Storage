namespace StudioLE.Patterns;

/// <summary>
/// Get <typeparamref name="TResult"/> from <typeparamref name="TSource"/>
/// using a <see href="https://refactoring.guru/design-patterns/strategy">strategy pattern</see>.
/// </summary>
public interface IStrategy<in TSource, out TResult>
{
    /// <summary>
    /// Get <typeparamref name="TResult"/> from <typeparamref name="TSource"/>.
    /// </summary>
    /// <param name="source">The source to use.</param>
    /// <returns>The resultant <typeparamref name="TResult"/>.</returns>
    public TResult Execute(TSource source);
}
