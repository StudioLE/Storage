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
[Obsolete("Use IFileWriter instead.")]
public interface IFileWriter
{
    /// <summary>
    /// Write the stream content to the storage system.
    /// </summary>
    /// <param name="path">The path including filename and extension.</param>
    /// <param name="stream">The stream to write. The implementation will take care of disposal.</param>
    /// <returns>
    /// The Uniform Resource Locator (URL) of the file written to the storage system,
    /// or <see langword="null"/> if the file could not be written.
    /// </returns>
    Task<string?> Write(string path, Stream stream);
}
