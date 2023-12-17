using Microsoft.Extensions.Logging;

namespace StudioLE.Storage;

/// <summary>
/// A strategy to store files in the local file system and serve them from Cascade.Server
/// </summary>
public class ServerStorageStrategy : IStorageStrategy
{
    private readonly string _directory = Path.GetTempPath();
    private readonly ILogger<ServerStorageStrategy> _logger;

    /// <summary>
    /// DI constructor for <see cref="ServerStorageStrategy"/>.
    /// </summary>
    public ServerStorageStrategy(ILogger<ServerStorageStrategy> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<Uri?> WriteAsync(string fileName, Stream stream)
    {
        try
        {
            string absolutePath = Path.Combine(_directory, fileName);
            if (File.Exists(absolutePath))
            {
                _logger.LogError("Failed to write to file storage. The file already exists.");
                return null;
            }
            using FileStream fileStream = new(absolutePath, FileMode.Create, FileAccess.Write);
            await stream.CopyToAsync(fileStream);
            stream.Close();
            stream.Dispose();
            string uri = VisualizationConfiguration.BaseAddress + "/" + VisualizationConfiguration.StorageRoute + "/" + fileName;
            return new(uri);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to write to file storage.");
            return null;
        }
    }

    // TODO: Replace Configuration with IOption DI constructor, with default overload.
    private static class VisualizationConfiguration
    {
        /// <summary>
        /// The path of the asset API endpoint.
        /// </summary>
        public const string BaseAddress = "http://localhost:3000";

        /// <summary>
        /// The path of the storage API endpoint.
        /// </summary>
        public const string StorageRoute = "storage";
    }

}
