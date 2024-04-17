using Microsoft.Extensions.Logging;

namespace StudioLE.Storage.Files;

/// <summary>
/// Options for <see cref="PhysicalFileWriter"/>
/// </summary>
public class PhysicalFileReaderOptions
{
    /// <summary>
    /// The directory to write files to.
    /// </summary>
    public string RootDirectory { get; set; } = Path.GetTempPath();

    /// <summary>
    /// The log level to use when logging errors.
    /// </summary>
    public LogLevel LogLevel { get; set; } = LogLevel.Error;
}
