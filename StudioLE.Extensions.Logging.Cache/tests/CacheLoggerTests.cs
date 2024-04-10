using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace StudioLE.Extensions.Logging.Cache.Tests;

internal sealed class CacheLoggerTests
{
    [Test]
    public void CacheLogger_Log([Values] LogLevel logLevel)
    {
        // Arrange
        const string message = "Hello, world!";
        CacheLogger logger = new("ExampleContext", _ => { });

        // Act
        logger.Log(logLevel, message);

        // Assert
        Assert.That(logger.Logs.Count, Is.EqualTo(1));
        LogEntry log = logger.Logs.First();
        Assert.That(log.LogLevel, Is.EqualTo(logLevel));
        Assert.That(log.Message, Is.EqualTo(message));
    }
}
