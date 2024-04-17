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
    private readonly PhysicalFileReaderOptions _options;

    /// <summary>
    /// DI constructor for <see cref="PhysicalFileReader"/>.
    /// </summary>
    public PhysicalFileReader(ILogger<PhysicalFileReader> logger, IOptions<PhysicalFileReaderOptions> options)
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
        {
            _logger.Log(_options.LogLevel, "Failed to read file. The root directory does not exist.");
            return null;
        }
        string absolutePath = Path.Combine(_options.RootDirectory, path);
        absolutePath = Path.GetFullPath(absolutePath);
        if (!absolutePath.StartsWith(_options.RootDirectory))
        {
            _logger.Log(_options.LogLevel, "Failed to read file. The path is outside the root directory.");
            return null;
        }
        if (!File.Exists(absolutePath))
        {
            _logger.Log(_options.LogLevel, "Failed to read file. The file does not exist.");
            return null;
        }
        return File.OpenRead(absolutePath);
    }
}
