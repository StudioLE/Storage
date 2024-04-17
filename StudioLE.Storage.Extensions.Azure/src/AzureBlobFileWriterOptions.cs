namespace StudioLE.Storage.Extensions.Azure;

/// <summary>
/// Options for <see cref="AzureBlobFileWriter"/>.
/// </summary>
public class AzureBlobFileWriterOptions
{
    /// <summary>
    /// The connection string.
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// The name of the container.
    /// </summary>
    public string Container { get; set; } = string.Empty;
}
