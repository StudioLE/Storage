namespace StudioLE.Storage.Files;

/// <summary>
/// Write a file to a storage system.
/// </summary>
/// <remarks>
/// Follows a strategy design pattern, by abstracting to an interface the caller does not need to
/// know anything about the implementation.
/// Typically, the implementation will be injected into the caller using dependency injection.
/// </remarks>
/// <seealso href="https://refactoring.guru/design-patterns/strategy"/>
/// <seealso href="https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection"/>
public interface IFileWriter
{
    /// <summary>
    /// Open a stream to write to the storage system.
    /// </summary>
    /// <param name="path">The path including filename and extension.</param>
    /// <param name="uri">The Uniform Resource Locator (URL) of the file written to the storage system.</param>
    /// <returns>
    /// The open and writeable stream, or <see langword="null"/> if the stream can't be opened.
    /// </returns>
    Task<Stream?> Open(string path, out string uri);
}
