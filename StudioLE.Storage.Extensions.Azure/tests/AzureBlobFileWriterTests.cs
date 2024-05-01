using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using StudioLE.Extensions.Logging.Cache;
using StudioLE.Extensions.System;
using StudioLE.Storage.Media;

namespace StudioLE.Storage.Extensions.Azure.Tests;

internal sealed class AzureBlobFileWriterTests
{
    [Test]
    [Explicit("Requires Azurite")]
    public async Task AzureBlobFileWriter_OpenWrite()
    {
        // Arrange
        const string text = "Hello, world.";
        CacheLoggerProvider cache = new();
        LoggerFactory loggerFactory = new(new[] { cache });
        ILogger<AzureBlobFileWriter> logger = loggerFactory.CreateLogger<AzureBlobFileWriter>();
        IOptions<AzureBlobFileWriterOptions> options = Options.Create(new AzureBlobFileWriterOptions
        {
            ConnectionString = "UseDevelopmentStorage=true",
            Container = "assets"
        });
        IMediaTypeProvider mediaTypeProvider = MediaTypeProvider.CreateDefault();
        AzureBlobFileWriter fileWriter = new(logger, options, mediaTypeProvider);
        string fileName = Guid.NewGuid() + ".txt";
        string? uri;

        // Act
        await using (Stream? stream = await fileWriter.OpenWrite(fileName, out uri))
        {
            if (stream is null)
                throw new("Stream is null");
            await using StreamWriter writer = new(stream);
            await writer.WriteAsync(text);
        }

        // Preview
        if (cache.Logs.Count != 0)
            Console.WriteLine(cache.Logs.Select(x => x.Message).Join());

        // Assert
        Assert.That(uri, Is.Not.Null);
        // TODO: Validate the content of the file.
    }
}
