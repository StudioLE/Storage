using Microsoft.Extensions.Logging;

namespace StudioLE.Extensions.Logging.Console;

/// <summary>
/// Methods to help with <see cref="BasicConsoleFormatter"/>.
/// </summary>
public static class BasicConsoleFormatterExtensions
{
    /// <summary>
    /// Add a <see cref="Microsoft.Extensions.Logging.Console.ConsoleLogger"/> with a <see cref="BasicConsoleFormatter"/>.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="logToStandardError">Should logs be output to standard error?</param>
    /// <returns>The builder.</returns>
    public static ILoggingBuilder AddBasicConsole(this ILoggingBuilder builder, bool logToStandardError = false)
    {
        return builder
            .AddConsole(options =>
            {
                options.FormatterName = BasicConsoleFormatter.FormatterName;
                if (logToStandardError)
                    options.LogToStandardErrorThreshold = LogLevel.Trace;
            })
            .AddConsoleFormatter<BasicConsoleFormatter, BasicConsoleFormatterOptions>();
    }

    /// <summary>
    /// Add a <see cref="Microsoft.Extensions.Logging.Console.ConsoleLogger"/> with a <see cref="BasicConsoleFormatter"/>.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="logToStandardError">Should logs be output to standard error?</param>
    /// <param name="configure">A delegate to configure the <see cref="BasicConsoleFormatterOptions"/>.</param>
    /// <returns>The builder.</returns>
    public static ILoggingBuilder AddBasicConsole(this ILoggingBuilder builder, Action<BasicConsoleFormatterOptions> configure, bool logToStandardError = false)
    {
        return builder
            .AddConsole(options =>
            {
                options.FormatterName = BasicConsoleFormatter.FormatterName;
                if (logToStandardError)
                    options.LogToStandardErrorThreshold = LogLevel.Trace;

            })
            .AddConsoleFormatter<BasicConsoleFormatter, BasicConsoleFormatterOptions>(configure);
    }
}
