namespace StudioLE.Serialization.Parsing;

/// <summary>
/// A <see href="https://refactoring.guru/design-patterns/strategy">strategy</see> to parse a string
/// to a target <see cref="Type"/> <see cref="object"/>.
/// </summary>
public interface IParser
{
    /// <summary>
    /// Determine if <paramref name="target"/> can be parsed.
    /// </summary>
    /// <param name="target">The type to parse to</param>
    /// <returns>
    /// <see langword = "true"/> if <paramref name="target"/> can be parsed; otherwise <see langword = "false"/>.
    /// </returns>
    bool CanParse(Type target);

    /// <summary>
    /// Parse <paramref name="source"/> to <paramref name="target"/>.
    /// </summary>
    /// <param name="source">The string to parse.</param>
    /// <param name="target">The type to parse to</param>
    /// <returns><paramref name="source"/> parsed to <paramref name="target"/> or <see langword="null"/> if not possible.</returns>
    object? Parse(string source, Type target);
}
