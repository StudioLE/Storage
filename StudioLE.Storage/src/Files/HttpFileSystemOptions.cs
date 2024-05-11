using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace StudioLE.Storage.Files;

/// <summary>
/// Options for <see cref="HttpFileReader"/>.
/// </summary>
public class HttpFileSystemOptions
{
    /// <summary>
    /// The log level to use when logging errors.
    /// </summary>
    [Required]
    public string BaseAddress { get; set; } = string.Empty;

    /// <summary>
    /// The log level to use when logging errors.
    /// </summary>
    public LogLevel LogLevel { get; set; } = LogLevel.Debug;
}
