namespace StudioLE.Extensions.System.Collections;

/// <summary>
/// Methods to help with <see cref="IReadOnlyCollection{T}"/>.
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    /// Determine if the collection has an element at <paramref name="index"/>.
    /// </summary>
    /// <param name="this">The collection to search.</param>
    /// <param name="index">The index to check.</param>
    /// <typeparam name="T">The type of the collection.</typeparam>
    /// <returns><see langword="true"/> if the index exists.</returns>
    public static bool HasIndex<T>(this IReadOnlyCollection<T> @this, int index)
    {
        return index >= 0 && index < @this.Count;
    }

    /// <summary>
    /// Try and get the <typeparamref name="T"/> at <paramref name="index"/>.
    /// </summary>
    /// <param name="this">The collection to search.</param>
    /// <param name="index">The index to retrieve.</param>
    /// <param name="element">The retrieve <typeparamref name="T"/> or <see langword="null"/> if it does not exist.</param>
    /// <typeparam name="T">The type of the collection.</typeparam>
    /// <returns><see langword="true"/> if the element is returned.</returns>
    public static bool TryGetAt<T>(this IReadOnlyCollection<T> @this, int index, out T? element)
    {
        if (@this.HasIndex(index))
        {
            element = @this.ElementAt(index);
            return true;
        }
        element = default;
        return false;
    }
    /// <summary>
    /// Get element at <paramref name="index"/>.
    /// If <paramref name="index"/> exceeds the collection count then search again from the begin.
    /// </summary>
    /// <param name="collection">The collection to search.</param>
    /// <param name="index">The index to retrieve.</param>
    /// <typeparam name="T">The type of the collection.</typeparam>
    /// <returns>The element at <paramref name="index"/>.</returns>
    /// <exception cref="ArgumentException">Thrown if the collection is empty.</exception>
    public static T ElementAtWithWrapping<T>(this IReadOnlyCollection<T> collection, int index)
    {
        if (collection.Count == 0)
            throw new ArgumentException($"{nameof(ElementAtWithWrapping)} failed. The collection was empty.", nameof(collection));
        int wrappedIndex = index % collection.Count;
        return collection.ElementAt(wrappedIndex);
    }
}
