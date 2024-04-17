using Azure.Core;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using StudioLE.Extensions.System.IO;

namespace StudioLE.Storage.Blob;

/// <summary>
/// A strategy to store files in Azure Blob Storage.
/// </summary>
[Obsolete("Use IFileWriter instead.")]
public class BlobStorageStrategy : IStorageStrategy
{
    private const string BlobConnectionString = "UseDevelopmentStorage=true";
    private const string BlobContainer = "assets";
    private static readonly BlobClientOptions _blobOptions = new()
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
    private readonly ILogger<BlobStorageStrategy> _logger;
    private readonly BlobContainerClient _container = new(BlobConnectionString, BlobContainer, _blobOptions);

    /// <summary>
    /// DI constructor for <see cref="BlobStorageStrategy"/>.
    /// </summary>
    public BlobStorageStrategy(ILogger<BlobStorageStrategy> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<Uri?> WriteAsync(string fileName, Stream stream)
    {
        try
        {
            BlobClient blob = _container.GetBlobClient(fileName);
            BlobHttpHeaders headers = new()
            {
                ContentType = fileName.GetContentTypeByExtension() ?? "application/octet-stream"
            };
            await blob.UploadAsync(stream, headers);
            stream.Close();
            stream.Dispose();
            return blob.Uri;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to write to blob storage.");
            return null;
        }
    }
}
