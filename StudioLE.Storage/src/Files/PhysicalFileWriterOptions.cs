using Microsoft.Extensions.Logging;

namespace StudioLE.Storage.Files;

/// <summary>
/// Options for <see cref="PhysicalFileWriter"/>
/// </summary>
public class PhysicalFileWriterOptions
{
    /// <summary>
    /// The directory to write files to.
    /// </summary>
    public string RootDirectory { get; set; } = Path.GetTempPath();

    /// <summary>
    /// Should the file be overwritten if it already exists?
    /// </summary>
    public bool AllowOverwrite { get; set; } = false;

    /// <summary>
    /// Can the path include sub directories?
    /// </summary>
    public bool AllowSubDirectories { get; set; } = false;

    /// <summary>
    /// Should sub directories be created if they don't exist?
    /// </summary>
    public bool AllowSubDirectoryCreation { get; set; } = true;

    /// <summary>
    /// The log level to use when logging errors.
    /// </summary>
    public LogLevel LogLevel { get; set; } = LogLevel.Error;
}
