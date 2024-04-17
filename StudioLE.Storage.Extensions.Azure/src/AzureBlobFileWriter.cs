using Azure.Core;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StudioLE.Extensions.System.IO;
using StudioLE.Storage.Files;

namespace StudioLE.Storage.Extensions.Azure;

/// <summary>
/// Write a file to Azure Blob Storage.
/// </summary>
public class AzureBlobFileWriter : IFileWriter
{
    private static readonly BlobClientOptions _clientOptions = new()
    {
        Retry =
        {
            MaxRetries = 0,
            NetworkTimeout = TimeSpan.FromMilliseconds(1000),
            Delay = TimeSpan.Zero,
            MaxDelay = TimeSpan.Zero,
            Mode = RetryMode.Fixed
        }
    };
    private readonly ILogger<AzureBlobFileWriter> _logger;
    private readonly IOptions<AzureBlobFileWriterOptions> _options;
    private readonly BlobContainerClient _container;

    /// <summary>
    /// DI constructor for <see cref="AzureBlobFileWriter"/>.
    /// </summary>
    public AzureBlobFileWriter(ILogger<AzureBlobFileWriter> logger, IOptions<AzureBlobFileWriterOptions> options)
    {
        _logger = logger;
        _options = options;
        _container = new(_options.Value.ConnectionString, _options.Value.Container, _clientOptions);
    }

    /// <inheritdoc/>
    public async Task<string?> Write(string path, Stream stream)
    {
        try
        {
            BlobClient blob = _container.GetBlobClient(path);
            BlobHttpHeaders headers = new()
            {
                ContentType = path.GetContentTypeByExtension() ?? "application/octet-stream"
            };
            await blob.UploadAsync(stream, headers);
            stream.Close();
            stream.Dispose();
            return blob.Uri.AbsoluteUri;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to write to blob storage.");
            return null;
        }
    }
}
