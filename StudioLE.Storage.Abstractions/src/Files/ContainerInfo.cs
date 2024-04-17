namespace StudioLE.Storage.Files;

/// <inheritdoc cref="IContainerInfo"/>
public readonly record struct ContainerInfo : IContainerInfo
{
    /// <inheritdoc/>
    public string Project { get; init; } = string.Empty;

    /// <inheritdoc/>
    public string Originator { get; init; } = string.Empty;

    /// <inheritdoc/>
    public string VolumeOrSystem { get; init; } = string.Empty;

    /// <inheritdoc/>
    public string LevelOrLocation { get; init; } = string.Empty;

    /// <inheritdoc/>
    public string Type { get; init; } = string.Empty;

    /// <inheritdoc/>
    public string Role { get; init; } = string.Empty;

    /// <inheritdoc/>
    public string Number { get; init; } = string.Empty;

    /// <inheritdoc/>
    public string Status { get; init; } = string.Empty;

    /// <inheritdoc/>
    public string Revision { get; init; } = string.Empty;

    /// <summary>
    /// Methods to help with <see cref="ContainerInfo"/>.
    /// </summary>
    public ContainerInfo()
    {
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        string id = $"{Project}-{Originator}-{VolumeOrSystem}-{LevelOrLocation}-{Type}-{Role}-{Number}";
        if (string.IsNullOrEmpty(Status))
            return id;
        return string.IsNullOrEmpty(Revision)
            ? $"{id}-{Status}"
            : $"{id}-{Status}-{Revision}";
    }
}
