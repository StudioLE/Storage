using Microsoft.Extensions.Logging.Console;

namespace StudioLE.Extensions.Logging.Console;

/// <summary>
/// Options for the <see cref="BasicConsoleFormatter"/>.
/// </summary>
public class BasicConsoleFormatterOptions : ConsoleFormatterOptions
{
    /// <summary>
    /// Should the ANSI console colors be disabled?
    /// </summary>
    public bool DisableColors { get; set; } = false;
}
