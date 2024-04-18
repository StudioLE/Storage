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
        IOptions<AzureBlobFileWriterOptions> options1 = options;
        _container = new(options1.Value.ConnectionString, options1.Value.Container, _clientOptions);
    }

    /// <inheritdoc/>
    public async Task<string?> Write(string path, Stream stream)
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
