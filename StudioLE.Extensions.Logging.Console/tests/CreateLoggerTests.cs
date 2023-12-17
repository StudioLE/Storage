using Example;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using NUnit.Framework;

namespace StudioLE.Extensions.Logging.Console.Tests;

internal sealed class CreateLoggerTests
{
    [Test]
    [Explicit("No assertions")]
    public void CreateLogger_simple()
    {
        // Arrange
        const LogLevel logLevel = LogLevel.Error;
        ILogger<ExampleContext> defaultLogger = CreateLogger.Simple<ExampleContext>();
        ILogger<ExampleContext> singleLineLogger = CreateLogger.Simple<ExampleContext>(options =>
        {
            options.SingleLine = true;
            options.ColorBehavior = LoggerColorBehavior.Enabled;
        });
        ILogger<ExampleContext> withoutColorLogger = CreateLogger.Simple<ExampleContext>(options =>
        {
            options.ColorBehavior = LoggerColorBehavior.Disabled;
        });

        // Act
        defaultLogger.Log(logLevel, $"This is a default log. {Environment.NewLine}With an additional line.");
        singleLineLogger.Log(logLevel, $"This is a log on a single line. {Environment.NewLine}With an additional line.");
        withoutColorLogger.Log(logLevel, $"This is a log without color. {Environment.NewLine}With an additional line.");

        // Assert
    }
}
