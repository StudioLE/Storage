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
    /// <returns>The builder.</returns>
    public static ILoggingBuilder AddBasicConsole(this ILoggingBuilder builder)
    {
        return builder
            .AddConsole(options => options.FormatterName = BasicConsoleFormatter.FormatterName)
            .AddConsoleFormatter<BasicConsoleFormatter, BasicConsoleFormatterOptions>();
    }

    /// <summary>
    /// Add a <see cref="Microsoft.Extensions.Logging.Console.ConsoleLogger"/> with a <see cref="BasicConsoleFormatter"/>.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="configure">A delegate to configure the <see cref="BasicConsoleFormatterOptions"/>.</param>
    /// <returns>The builder.</returns>
    public static ILoggingBuilder AddBasicConsole(this ILoggingBuilder builder, Action<BasicConsoleFormatterOptions> configure)
    {
        return builder
            .AddConsole(options => options.FormatterName = BasicConsoleFormatter.FormatterName)
            .AddConsoleFormatter<BasicConsoleFormatter, BasicConsoleFormatterOptions>(configure);
    }
}
