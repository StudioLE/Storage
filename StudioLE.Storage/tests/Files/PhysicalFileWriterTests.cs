using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using StudioLE.Extensions.Logging.Cache;
using StudioLE.Extensions.System;
using StudioLE.Storage.Files;
using StudioLE.Storage.Paths;

namespace StudioLE.Storage.Tests.Files;

internal sealed class PhysicalFileWriterTests
{
    [Test]
    public async Task PhysicalFileWriter_Write()
    {
        // Arrange
        CacheLoggerProvider cache = new();
        LoggerFactory loggerFactory = new(new[] { cache });
        ILogger<PhysicalFileWriter> logger = loggerFactory.CreateLogger<PhysicalFileWriter>();
        IOptions<PhysicalFileWriterOptions> options = Options.Create(new PhysicalFileWriterOptions());
        PhysicalFileWriter fileWriter = new (logger, options);
        MemoryStream stream = new();
        StreamWriter writer = new(stream);
        await writer.WriteAsync("Hello, world.");
        await writer.FlushAsync();
        stream.Seek(0, SeekOrigin.Begin);
        string fileName = Guid.NewGuid() + ".txt";

        // Act
        string? uri = await fileWriter.Write(fileName, stream);
        if (cache.Logs.Count != 0)
            Console.WriteLine(cache.Logs.Select(x => x.Message).Join());

        // Assert
        Assert.That(uri, Is.Not.Null);
        Assert.That(FileUriHelpers.IsFileUri(uri!), "Uri is file");
        Assert.That(FileUriHelpers.Exists(uri!), "File exists");
    }
}
