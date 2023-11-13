using StudioLE.Results;

namespace StudioLE.Storage;

/// <summary>
/// A strategy to store files.
/// </summary>
public interface IStorageStrategy
{
    /// <summary>
    /// Write a file asynchronously via a stream.
    /// </summary>
    Task<IResult<Uri>> WriteAsync(string fileName, Stream stream);
}
