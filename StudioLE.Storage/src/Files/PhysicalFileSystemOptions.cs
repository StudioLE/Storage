using Microsoft.Extensions.Logging;

namespace StudioLE.Storage.Files;

/// <summary>
/// Options for <see cref="PhysicalFileReader"/>, <see cref="PhysicalFileWriter"/>, and <see cref="PhysicalDirectoryReader"/>.
/// </summary>
public class PhysicalFileSystemOptions
{
    /// <summary>
    /// The directory to read files to.
    /// </summary>
    public string RootDirectory { get; set; } = Directory.GetCurrentDirectory();

    /// <summary>
    /// The log level to use when logging errors.
    /// </summary>
    public LogLevel LogLevel { get; set; } = LogLevel.Error;
}
