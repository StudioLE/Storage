namespace StudioLE.Storage;

/// <summary>
/// A <see href="https://refactoring.guru/design-patterns/strategy">strategy pattern</see> to store files.
/// </summary>
public interface IStorageStrategy
{
    /// <summary>
    /// Write a file asynchronously via a stream.
    /// </summary>
    Task<Uri?> WriteAsync(string fileName, Stream stream);
}
