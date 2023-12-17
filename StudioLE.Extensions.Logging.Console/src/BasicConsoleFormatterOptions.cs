using Microsoft.Extensions.Logging.Console;

namespace StudioLE.Extensions.Logging.Console;

public class BasicConsoleFormatterOptions : ConsoleFormatterOptions
{
    public bool DisableColors { get; set; } = false;
}
