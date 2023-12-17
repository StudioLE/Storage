using Example;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using NUnit.Framework;

namespace StudioLE.Extensions.Logging.Console.Tests;

internal sealed class ConsoleLoggerTests
{
    private readonly LogLevel[] _logLevels =
    {
        LogLevel.Trace,
        LogLevel.Debug,
        LogLevel.Information,
        LogLevel.Warning,
        LogLevel.Error,
        LogLevel.Critical,
        LogLevel.None,
        (LogLevel)10
    };

    [Test]
    [Explicit("No assertions")]
    public void ConsoleLogger_SimpleConsoleFormatter()
    {
        // Arrange
        ILoggerFactory factory = LoggerFactory.Create(builder => builder
            .SetMinimumLevel(LogLevel.Trace)
            .AddSimpleConsole(options =>
            {
                options.ColorBehavior = LoggerColorBehavior.Enabled;
            }));
        ILogger<ExampleContext> colorLogger = factory.CreateLogger<ExampleContext>();
        factory = LoggerFactory.Create(builder => builder
            .SetMinimumLevel(LogLevel.Trace)
            .AddSimpleConsole(options =>
            {
                options.ColorBehavior = LoggerColorBehavior.Disabled;
            }));
        ILogger<ExampleContext> withoutColorLogger = factory.CreateLogger<ExampleContext>();
        factory = LoggerFactory.Create(builder => builder
            .SetMinimumLevel(LogLevel.Trace)
            .AddSimpleConsole(options =>
            {
                options.SingleLine = true;
                options.ColorBehavior = LoggerColorBehavior.Enabled;
            }));
        ILogger<ExampleContext> singleLineLogger = factory.CreateLogger<ExampleContext>();

        // Act
        foreach (LogLevel logLevel in _logLevels)
        {
            colorLogger.Log(logLevel, $"This is a {logLevel} log with color.{Environment.NewLine}With an additional line.");
            withoutColorLogger.Log(logLevel, $"This is a {logLevel} log without color.{Environment.NewLine}With an additional line.");
            singleLineLogger.Log(logLevel, $"This is a {logLevel} log on a single line.{Environment.NewLine}With an additional line.");
        }

        // Assert
    }

    [Test]
    [Explicit("No assertions")]
    public void ConsoleLogger_BasicConsoleFormatter()
    {
        // Arrange
        ILoggerFactory factory = LoggerFactory.Create(builder => builder
            .SetMinimumLevel(LogLevel.Trace)
            .AddBasicConsole());
        ILogger<ExampleContext> colorLogger = factory.CreateLogger<ExampleContext>();
        factory = LoggerFactory.Create(builder => builder
            .SetMinimumLevel(LogLevel.Trace)
            .AddBasicConsole(options =>
            {
                options.DisableColors = true;
            }));
        ILogger<ExampleContext> withoutColorLogger = factory.CreateLogger<ExampleContext>();

        // Act
        foreach (LogLevel logLevel in _logLevels)
            withoutColorLogger.Log(logLevel, $"This is a {logLevel} log without color.");
        foreach (LogLevel logLevel in _logLevels)
            colorLogger.Log(logLevel, $"This is a {logLevel} log with color.");

        // Assert
    }
}
