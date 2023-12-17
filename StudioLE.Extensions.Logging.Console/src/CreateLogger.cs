using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace StudioLE.Extensions.Logging.Console;

/// <summary>
/// Factory methods to create an <see cref="ILogger{T}"/> that logs to the console.
/// </summary>
public static class CreateLogger
{
    /// <summary>
    /// Create an <see cref="ILogger{T}"/> that logs to the console using a <see cref="SimpleConsoleFormatter"/>.
    /// </summary>
    /// <typeparam name="TScope">The scope of the logger.</typeparam>
    /// <returns>The created logger.</returns>
    public static ILogger<TScope> Simple<TScope>()
    {
        using ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddSimpleConsole();
        });
        return loggerFactory.CreateLogger<TScope>();
    }

    /// <summary>
    /// Create an <see cref="ILogger{T}"/> that logs to the console using a <see cref="SimpleConsoleFormatter"/>.
    /// </summary>
    /// <param name="configure">A delegate to configure the <see cref="SimpleConsoleFormatterOptions"/>.</param>
    /// <typeparam name="TScope">The scope of the logger.</typeparam>
    /// <returns>The created logger.</returns>
    public static ILogger<TScope> Simple<TScope>(Action<SimpleConsoleFormatterOptions> configure)
    {
        using ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddSimpleConsole(configure);
        });
        return loggerFactory.CreateLogger<TScope>();
    }
}
