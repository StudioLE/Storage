using Microsoft.Extensions.Logging;
using NUnit.Framework;
using StudioLE.Extensions.Logging.Cache;
using StudioLE.Extensions.System;
using StudioLE.Storage.Blob;
#pragma warning disable CS0618 // Type or member is obsolete

namespace StudioLE.Storage.Tests;

internal sealed class StorageStrategyTests
{
    [Test]
    public async Task FileStorageStrategy_WriteAsync()
    {
        // Arrange
        CacheLoggerProvider cache = new();
        LoggerFactory loggerFactory = new(new[] { cache });
        ILogger<FileStorageStrategy> logger = loggerFactory.CreateLogger<FileStorageStrategy>();
        IStorageStrategy storageStrategy = new FileStorageStrategy(logger);
        MemoryStream stream = new();
        StreamWriter writer = new(stream);
        writer.Write("Hello, world.");
        writer.Flush();
        stream.Seek(0, SeekOrigin.Begin);
        string fileName = Guid.NewGuid() + ".txt";

        // Act
        Uri? uri = await storageStrategy.WriteAsync(fileName, stream);
        if (cache.Logs.Count != 0)
            Console.WriteLine(cache.Logs.Select(x => x.Message).Join());

        // Assert
        Assert.That(uri, Is.Not.Null);
        Assert.That(uri!.IsFile, "Uri is file");
        Assert.That(File.Exists(uri.AbsolutePath), "File exists");
    }

    [Test]
    [Explicit("Requires Azurite")]
    [Category("Requires Azurite")]
    public async Task BlobStorageStrategy_WriteAsync()
    {
        // Arrange
        CacheLoggerProvider cache = new();
        LoggerFactory loggerFactory = new(new[] { cache });
        ILogger<BlobStorageStrategy> logger = loggerFactory.CreateLogger<BlobStorageStrategy>();
        IStorageStrategy storageStrategy = new BlobStorageStrategy(logger);
        MemoryStream stream = new();
        StreamWriter writer = new(stream);
        writer.Write("Hello, world.");
        writer.Flush();
        stream.Seek(0, SeekOrigin.Begin);
        string fileName = Guid.NewGuid() + ".txt";

        // Act
        Uri? uri = await storageStrategy.WriteAsync(fileName, stream);
        if (cache.Logs.Count != 0)
            Console.WriteLine(cache.Logs.Select(x => x.Message).Join());

        // Assert
        Assert.That(uri, Is.Not.Null);
        Assert.That(uri!.IsFile, Is.False, "Uri is file");
        Assert.That(File.Exists(uri.AbsolutePath), "File exists");
    }
}
