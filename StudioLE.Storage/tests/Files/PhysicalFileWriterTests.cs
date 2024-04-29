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
    public async Task PhysicalFileWriter_Open()
    {
        // Arrange
        const string text = "Hello, world.";
        CacheLoggerProvider cache = new();
        LoggerFactory loggerFactory = new(new[] { cache });
        ILogger<PhysicalFileWriter> logger = loggerFactory.CreateLogger<PhysicalFileWriter>();
        PhysicalFileSystemOptions systemOptions = new()
        {
            RootDirectory = Path.GetTempPath()
        };
        PhysicalFileWriterOptions writerOptions = new();
        PhysicalFileWriter fileWriter = new(logger, Options.Create(systemOptions), Options.Create(writerOptions));
        string fileName = Guid.NewGuid() + ".txt";
        string? uri;

        // Act
        await using (Stream? stream = await fileWriter.Open(fileName, out uri))
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
        Assert.That(FileUriHelpers.IsFileUri(uri), "Uri is file");
        Assert.That(FileUriHelpers.Exists(uri), "File exists");
        string? filePath = FileUriHelpers.GetPath(uri);
        string fileContent = await File.ReadAllTextAsync(filePath!);
        Assert.That(fileContent, Is.EqualTo(text));
    }

    [Test]
    public async Task PhysicalFileWriter_Write_WithSubDirectory()
    {
        // Arrange
        const string text = "Hello, world.";
        CacheLoggerProvider cache = new();
        LoggerFactory loggerFactory = new(new[] { cache });
        ILogger<PhysicalFileWriter> logger = loggerFactory.CreateLogger<PhysicalFileWriter>();
        PhysicalFileSystemOptions systemOptions = new()
        {
            RootDirectory = Path.GetTempPath()
        };
        PhysicalFileWriterOptions writerOptions = new()
        {
            AllowSubDirectories = true,
            AllowSubDirectoryCreation = true
        };
        PhysicalFileWriter fileWriter = new(logger, Options.Create(systemOptions), Options.Create(writerOptions));
        string path = Guid.NewGuid() + "/" + Guid.NewGuid() + "/" + Guid.NewGuid() + ".txt";
        string? uri;

        // Act
        await using (Stream? stream = await fileWriter.Open(path, out uri))
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
        Assert.That(FileUriHelpers.IsFileUri(uri), "Uri is file");
        Assert.That(FileUriHelpers.Exists(uri), "File exists");
        string? filePath = FileUriHelpers.GetPath(uri);
        string fileContent = await File.ReadAllTextAsync(filePath!);
        Assert.That(fileContent, Is.EqualTo(text));
    }
}
