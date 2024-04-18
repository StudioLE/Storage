using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using StudioLE.Extensions.Logging.Cache;
using StudioLE.Extensions.System;
using StudioLE.Storage.Files;
using StudioLE.Storage.Tests.Resources;

namespace StudioLE.Storage.Tests.Files;

internal sealed class PhysicalDirectoryReaderTests
{
    private CacheLoggerProvider _cache;
    private PhysicalDirectoryReader _fileReader;

    [SetUp]
    public void Setup()
    {
        _cache = new();
        LoggerFactory loggerFactory = new(new[] { _cache });
        ILogger<PhysicalDirectoryReader> logger = loggerFactory.CreateLogger<PhysicalDirectoryReader>();
        string directory = Path.GetFullPath(ExampleHelpers.DirectoryPath);
        PhysicalFileSystemOptions options = new()
        {
            RootDirectory = directory
        };
        _fileReader = new(logger, Options.Create(options));
    }

    [Test]
    public async Task PhysicalDirectoryReader_Read()
    {
        // Arrange
        // Act
        IEnumerable<string>? result = await _fileReader.Read(string.Empty);
        string[]? files = result?.ToArray();

        // Preview
        if (_cache.Logs.Count != 0)
            Console.WriteLine(_cache.Logs.Select(x => x.Message).Join());

        // Assert
        Assert.That(files, Is.Not.Null);
        Assert.That(files!.Length, Is.EqualTo(ExampleHelpers.DirectoryFileCount));
        Assert.That(_cache.Logs.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task PhysicalDirectoryReader_Read_NotExist()
    {
        // Arrange
        // Act
        IEnumerable<string>? result = await _fileReader.Read("DoesNotExist");
        string[]? files = result?.ToArray();

        // Preview
        if (_cache.Logs.Count != 0)
            Console.WriteLine(_cache.Logs.Select(x => x.Message).Join());

        // Assert
        Assert.That(files, Is.Null);
        Assert.That(_cache.Logs.Count, Is.EqualTo(1));
    }

    [Test]
    public async Task PhysicalDirectoryReader_Read_File()
    {
        // Arrange
        // Act
        IEnumerable<string>? result = await _fileReader.Read(ExampleHelpers.FileName);
        string[]? files = result?.ToArray();

        // Preview
        if (_cache.Logs.Count != 0)
            Console.WriteLine(_cache.Logs.Select(x => x.Message).Join());

        // Assert
        Assert.That(files, Is.Null);
        Assert.That(_cache.Logs.Count, Is.EqualTo(1));
    }
}
