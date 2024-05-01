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
    public Task<Stream?> OpenWrite(string path, out string uri)
    {
        uri = string.Empty;
        if (!Directory.Exists(_systemOptions.RootDirectory))
            return Failed(path, "The root directory does not exist.");
        if (!_options.AllowSubDirectories && path != Path.GetFileName(path))
            return Failed(path, "Sub directories are not allowed.");
        string absolutePath = Path.Combine(_systemOptions.RootDirectory, path);
        absolutePath = Path.GetFullPath(absolutePath);
        if (!absolutePath.StartsWith(_systemOptions.RootDirectory))
            return Failed(path, "The path is outside the root directory.");
        if (!_options.AllowOverwrite && File.Exists(absolutePath))
            return Failed(path, "The file already exists.");
        string? directoryPath = Path.GetDirectoryName(absolutePath);
        if (!_options.AllowSubDirectoryCreation && directoryPath is not null && !Directory.Exists(directoryPath))
            return Failed(path, "The sub directory does not exist.");
        uri = FileUriHelpers.FromAbsolutePath(absolutePath);
        try
        {
            if (_options.AllowSubDirectoryCreation && directoryPath is not null && !Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            FileStream fileStream = new(absolutePath, FileMode.Create, FileAccess.Write);
            return Task.FromResult<Stream?>(fileStream);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Failed to write file: {path}. {e.GetType()} {e.Message}");
            return Task.FromResult<Stream?>(null);
        }
    }

    private Task<Stream?> Failed(string message, string path)
    {
        _logger.Log(_options.LogLevel, $"Failed to write file: {path}. {message}");
        return Task.FromResult<Stream?>(null);
    }
}
