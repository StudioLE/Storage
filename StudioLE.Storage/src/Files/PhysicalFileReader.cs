using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace StudioLE.Storage.Files;

/// <summary>
/// Read a file from the physical file system.
/// </summary>
/// <inheritdoc cref="IFileReader"/>
public class PhysicalFileReader : IFileReader
{
    private readonly ILogger<PhysicalFileReader> _logger;
    private readonly PhysicalFileSystemOptions _options;

    /// <summary>
    /// DI constructor for <see cref="PhysicalFileReader"/>.
    /// </summary>
    public PhysicalFileReader(ILogger<PhysicalFileReader> logger, IOptions<PhysicalFileSystemOptions> options)
    {
        _logger = logger;
        _options = options.Value;
    }

    /// <inheritdoc/>
    public Task<Stream?> Read(string path)
    {
        Stream? stream = ReadSync(path);
        return Task.FromResult(stream);
    }

    private Stream? ReadSync(string path)
    {
        if (!Directory.Exists(_options.RootDirectory))
            return Failed(path, "The root directory does not exist");
        string absolutePath = Path.Combine(_options.RootDirectory, path);
        absolutePath = Path.GetFullPath(absolutePath);
        if (!absolutePath.StartsWith(_options.RootDirectory))
            return Failed(path, "The path is outside the root directory");
        if (!File.Exists(absolutePath))
            return Failed(path, "The file does not exist");
        return File.OpenRead(absolutePath);
    }

    private Stream? Failed(string path, string message)
    {
        if(string.IsNullOrEmpty(message))
            _logger.Log(_options.LogLevel, "Failed to read file {Path}", path);
        else
            _logger.Log(_options.LogLevel, "Failed to read file {Path}. {Message}", path, message);
        return null;
    }
}
