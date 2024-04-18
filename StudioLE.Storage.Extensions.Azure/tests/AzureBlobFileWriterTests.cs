using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using StudioLE.Extensions.Logging.Cache;
using StudioLE.Extensions.System;
using StudioLE.Storage.Media;
using StudioLE.Storage.Paths;

namespace StudioLE.Storage.Extensions.Azure.Tests;

internal sealed class AzureBlobFileWriterTests
{
    [Test]
    [Explicit("Requires Azurite")]
    public async Task AzureBlobFileWriter_Write()
    {
        // Arrange
        CacheLoggerProvider cache = new();
        LoggerFactory loggerFactory = new(new[] { cache });
        ILogger<AzureBlobFileWriter> logger = loggerFactory.CreateLogger<AzureBlobFileWriter>();
        IOptions<AzureBlobFileWriterOptions> options = Options.Create(new AzureBlobFileWriterOptions()
        {
            ConnectionString = "UseDevelopmentStorage=true",
            Container = "assets"
        });
        IMediaTypeProvider mediaTypeProvider = MediaTypeProvider.CreateDefault();
        AzureBlobFileWriter fileWriter = new(logger, options, mediaTypeProvider);
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
        Assert.That(FileUriHelpers.IsFileUri(uri!), Is.False, "Uri is file");
        Assert.That(File.Exists(uri), "File exists");
    }
}
