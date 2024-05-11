using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace StudioLE.Storage.Files;

/// <summary>
/// Read a file from a web server.
/// </summary>
public class HttpFileReader : IFileReader
{
    private readonly ILogger<HttpFileReader> _logger;
    private readonly HttpFileSystemOptions _options;
    private readonly HttpFileSystemClient _client;

    /// <summary>
    /// DI constructor for <see cref="HttpFileReader"/>.
    /// </summary>
    public HttpFileReader(ILogger<HttpFileReader> logger, IOptions<HttpFileSystemOptions> options, HttpFileSystemClient client)
    {
        _logger = logger;
        _options = options.Value;
        _client = client;
    }

    /// <inheritdoc />
    public async Task<Stream?> Read(string path)
    {
        HttpResponseMessage? response = await _client.GetAsync(path);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsStreamAsync();
        if (response.StatusCode == HttpStatusCode.NotFound)
            return Failed(path, "The file does not exist");
        return Failed(path, response.ReasonPhrase);
    }

    private Stream? Failed(string path, string message)
    {
        if(string.IsNullOrEmpty(message))
            _logger.Log(_options.LogLevel, "Failed to read file {Path}", path);
        else
            _logger.Log(_options.LogLevel, "Failed to read file {Path}. {Message}", path, message);
        return null;
    }
}
