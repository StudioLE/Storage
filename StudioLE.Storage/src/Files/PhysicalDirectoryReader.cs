using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace StudioLE.Storage.Files;

/// <summary>
/// Read the file names of a directory from the physical file system.
/// </summary>
/// <inheritdoc cref="IDirectoryReader"/>
public class PhysicalDirectoryReader : IDirectoryReader
{
    private readonly ILogger<PhysicalDirectoryReader> _logger;
    private readonly PhysicalFileSystemOptions _options;

    /// <summary>
    /// DI constructor for <see cref="PhysicalDirectoryReader"/>.
    /// </summary>
    public PhysicalDirectoryReader(ILogger<PhysicalDirectoryReader> logger, IOptions<PhysicalFileSystemOptions> options)
    {
        _logger = logger;
        _options = options.Value;
    }

    /// <inheritdoc />
    public Task<IEnumerable<string>?> GetDirectoryNames(string path)
    {
        IEnumerable<string>? directories = GetDirectoryNamesSync(path);
        return Task.FromResult(directories);
    }

    /// <inheritdoc />
    public Task<IEnumerable<string>?> GetFileNames(string path)
    {
        IEnumerable<string>? files = GetFileNamesSync(path);
        return Task.FromResult(files);
    }

    private IEnumerable<string>? GetDirectoryNamesSync(string path)
    {
        string? absolutePath = GetAbsolutePath(path);
        if (absolutePath == null)
            return null;
        return Directory
            .EnumerateDirectories(absolutePath)
            .Select(Path.GetFileName);
    }

    private IEnumerable<string>? GetFileNamesSync(string path)
    {
        string? absolutePath = GetAbsolutePath(path);
        if (absolutePath == null)
            return null;
        return Directory
            .EnumerateFiles(absolutePath)
            .Select(Path.GetFileName);
    }

    private string? GetAbsolutePath(string path)
    {
        if (!Directory.Exists(_options.RootDirectory))
        {
            _logger.Log(_options.LogLevel, "Failed to read directory. The root directory does not exist.");
            return null;
        }
        string absolutePath = Path.Combine(_options.RootDirectory, path);
        absolutePath = Path.GetFullPath(absolutePath);
        if (!absolutePath.StartsWith(_options.RootDirectory))
        {
            _logger.Log(_options.LogLevel, "Failed to read directory. The path is outside the root directory.");
            return null;
        }
        if (!Directory.Exists(absolutePath))
        {
            _logger.Log(_options.LogLevel, "Failed to read directory. The directory does not exist.");
            return null;
        }
        return absolutePath;
    }
}
