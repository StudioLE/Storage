namespace StudioLE.Storage.Files;

/// <summary>
/// Read the file or directories names of a directory in a storage system.
/// </summary>
/// <remarks>
/// Follows a strategy design pattern, by abstracting to an interface the caller does not need to
/// know anything about the implementation.
/// Typically, the implementation will be injected into the caller using dependency injection.
/// </remarks>
/// <seealso href="https://refactoring.guru/design-patterns/strategy"/>
/// <seealso href="https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection"/>
public interface IDirectoryReader
{
    /// <summary>
    /// Read the directory names in a directory in a storage system.
    /// </summary>
    /// <param name="path">The path to read.</param>
    /// <returns>
    /// The directory names as an enumerable, or <see langword="null"/> if the path could not be read.
    /// </returns>
    Task<IEnumerable<string>?> GetDirectoryNames(string path);

    /// <summary>
    /// Read the file names in a directory in a storage system.
    /// </summary>
    /// <param name="path">The path to read.</param>
    /// <returns>
    /// The file names as an enumerable, or <see langword="null"/> if the path could not be read.
    /// </returns>
    Task<IEnumerable<string>?> GetFileNames(string path);
}
