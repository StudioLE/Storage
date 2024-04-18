using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StudioLE.Storage.Paths;

namespace StudioLE.Storage.Files;

/// <summary>
/// Write a file to the physical file system.
/// </summary>
/// <inheritdoc cref="IFileWriter"/>
public class PhysicalFileWriter : IFileWriter
{
    private readonly ILogger<PhysicalFileWriter> _logger;
    private readonly PhysicalFileSystemOptions _systemOptions;
    private readonly PhysicalFileWriterOptions _options;

    /// <summary>
    /// DI constructor for <see cref="PhysicalFileWriter"/>.
    /// </summary>
    public PhysicalFileWriter(
        ILogger<PhysicalFileWriter> logger,
        IOptions<PhysicalFileSystemOptions> systemOptions,
        IOptions<PhysicalFileWriterOptions> writerOptions)
    {
        _logger = logger;
        _systemOptions = systemOptions.Value;
        _options = writerOptions.Value;
    }

    /// <inheritdoc/>
    public async Task<string?> Write(string path, Stream stream)
    {
        if (!Directory.Exists(_systemOptions.RootDirectory))
        {
            _logger.Log(_options.LogLevel, "Failed to write file. The root directory does not exist.");
            return null;
        }
        if (!_options.AllowSubDirectories && path != Path.GetFileName(path))
        {
            _logger.Log(_options.LogLevel, "Failed to write file. Sub directories are not allowed.");
            return null;
        }
        string absolutePath = Path.Combine(_systemOptions.RootDirectory, path);
        absolutePath = Path.GetFullPath(absolutePath);
        if (!absolutePath.StartsWith(_systemOptions.RootDirectory))
        {
            _logger.Log(_options.LogLevel, "Failed to write file. The path is outside the root directory.");
            return null;
        }
        if (!_options.AllowOverwrite && File.Exists(absolutePath))
        {
            _logger.Log(_options.LogLevel, "Failed to write file. The file already exists.");
            return null;
        }
        string? directoryPath = Path.GetDirectoryName(absolutePath);
        if (!_options.AllowSubDirectoryCreation && directoryPath is not null && !Directory.Exists(directoryPath))
        {
            _logger.Log(_options.LogLevel, "Failed to write file. The sub directory does not exist.");
            return null;
        }
        try
        {
            if (_options.AllowSubDirectoryCreation && directoryPath is not null && !Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            using FileStream fileStream = new(absolutePath, FileMode.Create, FileAccess.Write);
            await stream.CopyToAsync(fileStream);
            stream.Close();
            stream.Dispose();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to write file.");
            return null;
        }
        string uri = FileUriHelpers.FromAbsolutePath(absolutePath);
        return uri;
    }
}
