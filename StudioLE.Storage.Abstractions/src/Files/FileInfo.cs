namespace StudioLE.Storage.Files;

/// <inheritdoc cref="IFileInfo"/>
public readonly record struct FileInfo : IFileInfo
{
    /// <inheritdoc />
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <inheritdoc />
    public string Name { get; init; } = string.Empty;

    /// <inheritdoc />
    public string Description { get; init; } = string.Empty;

    /// <inheritdoc />
    public string MediaType { get; init; } = string.Empty;

    /// <inheritdoc />
    public string Location { get; init; } = string.Empty;

    /// <inheritdoc />
    public bool Exists { get; init; } = false;

    /// <inheritdoc />
    public IContainerInfo? ContainerInformation { get; init; } = null;

    /// <summary>
    /// Create a new instance of <see cref="FileInfo"/>.
    /// </summary>
    public FileInfo()
    {
    }
}
