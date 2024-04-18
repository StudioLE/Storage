using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using StudioLE.Extensions.Logging.Cache;
using StudioLE.Extensions.System;
using StudioLE.Storage.Files;
using StudioLE.Storage.Tests.Resources;

namespace StudioLE.Storage.Tests.Files;

internal sealed class PhysicalFileReaderTests
{
    private readonly CacheLoggerProvider _cache;
    private readonly PhysicalFileReader _fileReader;

    public PhysicalFileReaderTests()
    {
        _cache = new();
        LoggerFactory loggerFactory = new(new[] { _cache });
        ILogger<PhysicalFileReader> logger = loggerFactory.CreateLogger<PhysicalFileReader>();
        string directory = Path.GetFullPath(ExampleHelpers.DirectoryPath);
        PhysicalFileSystemOptions options = new()
        {
            RootDirectory = directory
        };
        _fileReader = new(logger, Options.Create(options));
    }

    [Test]
    public async Task PhysicalFileReader_Read()
    {
        // Arrange
        // Act
        Stream? stream = await _fileReader.Read(ExampleHelpers.FileName);

        // Preview
        if (_cache.Logs.Count != 0)
            Console.WriteLine(_cache.Logs.Select(x => x.Message).Join());

        // Assert
        if (stream is null)
            throw new("Stream is null");
        StreamReader streamReader = new(stream);
        string content = await streamReader.ReadToEndAsync();
        Assert.That(content, Is.EqualTo(ExampleHelpers.FileContent));
        Assert.That(_cache.Logs.Count, Is.EqualTo(0));
    }


    [Test]
    public async Task PhysicalFileReader_Read_NotExist()
    {
        // Arrange
        // Act
        Stream? stream = await _fileReader.Read("FileDoesNotExist.txt");

        // Preview
        if (_cache.Logs.Count != 0)
            Console.WriteLine(_cache.Logs.Select(x => x.Message).Join());

        // Assert
        Assert.That(stream, Is.Null);
        Assert.That(_cache.Logs.Count, Is.EqualTo(1));
    }
}
