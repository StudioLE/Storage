using Microsoft.Extensions.Logging;

namespace StudioLE.Extensions.Logging.Console;

public static class BasicConsoleFormatterExtensions
{
    public static ILoggingBuilder AddBasicConsole(this ILoggingBuilder builder)
    {
        return builder
            .AddConsole(options => options.FormatterName = BasicConsoleFormatter.FormatterName)
            .AddConsoleFormatter<BasicConsoleFormatter, BasicConsoleFormatterOptions>();
    }

    public static ILoggingBuilder AddBasicConsole(this ILoggingBuilder builder, Action<BasicConsoleFormatterOptions> configure)
    {
        return builder
            .AddConsole(options => options.FormatterName = BasicConsoleFormatter.FormatterName)
            .AddConsoleFormatter<BasicConsoleFormatter, BasicConsoleFormatterOptions>(configure);
    }
}
