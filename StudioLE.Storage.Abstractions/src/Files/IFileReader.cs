namespace StudioLE.Storage.Files;

/// <summary>
/// Read a file from a storage system.
/// </summary>
/// <remarks>
/// Follows a strategy design pattern, by abstracting to an interface the caller does not need to
/// know anything about the implementation.
/// Typically, the implementation will be injected into the caller using dependency injection.
/// </remarks>
/// <seealso href="https://refactoring.guru/design-patterns/strategy"/>
/// <seealso href="https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection"/>
public interface IFileReader
{
    /// <summary>
    /// Read the content from the storage system as a stream.
    /// </summary>
    /// <param name="path">The path including filename and extension.</param>
    /// <returns>
    /// The file content as a stream, or <see langword="null"/> if the file could not be read.
    /// </returns>
    Task<Stream?> Read(string path);
}
