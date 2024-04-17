using System.Net;
using Microsoft.Extensions.Logging;

namespace StudioLE.Storage.Files;

/// <summary>
/// Read a file from a web server.
/// </summary>
public class HttpFileReader : IFileReader
{
    private readonly ILogger<HttpFileReader> _logger;
    private readonly HttpClient _client;

    /// <summary>
    /// DI constructor for <see cref="HttpFileReader"/>.
    /// </summary>
    public HttpFileReader(ILogger<HttpFileReader> logger, HttpClient httpClient)
    {
        _logger = logger;
        _client = httpClient;
    }

    /// <inheritdoc />
    public async Task<Stream?> Read(string path)
    {
        HttpResponseMessage? response = await _client.GetAsync(path);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsStreamAsync();
        if(response.StatusCode == HttpStatusCode.NotFound)
            _logger.Log(LogLevel.Error, "Failed to read file. The file does not exist.");
        else
            _logger.Log(LogLevel.Error, "Failed to read file. The server returned an error.");
        return null;
    }
}
