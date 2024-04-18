using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using StudioLE.Extensions.Logging.Cache;
using StudioLE.Extensions.System;
using StudioLE.Storage.Files;
using StudioLE.Storage.Tests.Resources;

namespace StudioLE.Storage.Tests.Files;

internal sealed class HttpFileReaderTests : IAsyncDisposable
{
    private const string StaticHostBaseAddress = "http://localhost:5683";
    private readonly WebApplication _staticHost;
    private readonly CacheLoggerProvider _cache;
    private readonly HttpFileReader _fileReader;

    public HttpFileReaderTests()
    {
        _staticHost = CreateStaticHost();
        _cache = new();
        LoggerFactory loggerFactory = new(new[] { _cache });
        ILogger<HttpFileReader> logger = loggerFactory.CreateLogger<HttpFileReader>();
        HttpClient client = new()
        {
            BaseAddress = new(StaticHostBaseAddress)
        };
        _fileReader = new(logger, client);
    }

    [OneTimeSetUp]
    public async Task SetUp()
    {
        await _staticHost.StartAsync();
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
        Assert.That(content, Is.EqualTo(ExampleHelpers.FileContent).Or.EqualTo(ExampleHelpers.FileContentCrLf));
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

    private static WebApplication CreateStaticHost()
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(["--urls", StaticHostBaseAddress]);
        WebApplication app = builder.Build();
        string absolutePath = Path.GetFullPath(ExampleHelpers.DirectoryPath);
        PhysicalFileProvider staticFiles = new(absolutePath);
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = staticFiles,
            ServeUnknownFileTypes = true
        });
        return app;
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await _staticHost.StopAsync();
    }
}
