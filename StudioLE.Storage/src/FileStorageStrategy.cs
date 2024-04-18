using Microsoft.Extensions.Logging;

namespace StudioLE.Storage;

/// <summary>
/// A strategy to store files in the local file system.
/// using a <see href="https://refactoring.guru/design-patterns/strategy">strategy pattern</see>.
/// </summary>
[Obsolete("Replaced by PhysicalFileWriter")]
public class FileStorageStrategy : IStorageStrategy
{
    private readonly string _directory = Path.GetTempPath();
    private readonly ILogger<FileStorageStrategy> _logger;

    /// <summary>
    /// DI constructor for <see cref="FileStorageStrategy"/>.
    /// </summary>
    public FileStorageStrategy(ILogger<FileStorageStrategy> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<Uri?> WriteAsync(string fileName, Stream stream)
    {
        try
        {
            string absolutePath = Path.Combine(_directory, fileName);
            if (File.Exists(absolutePath))
            {
                _logger.LogError("Failed to write to file storage. The file already exists.");
                return null;
            }
            using FileStream fileStream = new(absolutePath, FileMode.Create, FileAccess.Write);
            await stream.CopyToAsync(fileStream);
            stream.Close();
            stream.Dispose();
            return new(absolutePath);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to write to file storage.");
            return null;
        }
    }
}
