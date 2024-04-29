using Azure.Core;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StudioLE.Storage.Files;
using StudioLE.Storage.Media;
using StudioLE.Storage.Paths;

namespace StudioLE.Storage.Extensions.Azure;

/// <summary>
/// Write a file to Azure Blob Storage.
/// </summary>
public class AzureBlobFileWriter : IFileWriter
{
    private const bool AllowOverwrite = false;
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
    private readonly IMediaTypeProvider _mediaTypeProvider;
    private readonly BlobContainerClient _container;

    /// <summary>
    /// DI constructor for <see cref="AzureBlobFileWriter"/>.
    /// </summary>
    public AzureBlobFileWriter(
        ILogger<AzureBlobFileWriter> logger,
        IOptions<AzureBlobFileWriterOptions> options,
        IMediaTypeProvider mediaTypeProvider)
    {
        _logger = logger;
        _mediaTypeProvider = mediaTypeProvider;
        _container = new(options.Value.ConnectionString, options.Value.Container, _clientOptions);
    }

    /// <inheritdoc/>
    public Task<Stream?> Open(string path, out string uri)
    {
        try
        {
            string? fileExtension = UriHelpers.GetFileExtension(path);
            string? contentType = null;
            if(fileExtension is not null)
                contentType = _mediaTypeProvider.ByFileExtension(fileExtension)?.Type;
            contentType ??= "application/octet-stream";
            BlobClient blob = _container.GetBlobClient(path);
            BlobHttpHeaders headers = new()
            {
                ContentType = contentType
            };
            BlobOpenWriteOptions options = new()
            {
                HttpHeaders = headers
            };
            uri = blob.Uri.AbsoluteUri;
            return blob.OpenWriteAsync(AllowOverwrite, options);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to write to blob storage.");
            uri = string.Empty;
            return Task.FromResult<Stream?>(null);
        }
    }
}
