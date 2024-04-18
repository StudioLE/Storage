namespace StudioLE.Storage.Media;

/// <summary>
/// A media type that corresponds to a file extension.
/// </summary>
/// <seealso href="https://en.wikipedia.org/wiki/Media_type"/>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Glossary/MIME_type"/>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Type"/>
public readonly record struct MediaType
{
    /// <summary>
    /// The file extension (including . prefix).
    /// </summary>
    public string Extension { get; init; } = string.Empty;

    /// <summary>
    /// The kind of document.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// The media type.
    /// </summary>
    public string Type { get; init; } = string.Empty;

    /// <summary>
    /// Create a new instance of <see cref="MediaType"/>.
    /// </summary>
    public MediaType()
    {
    }

    /// <summary>
    /// Create a new instance of <see cref="MediaType"/>.
    /// </summary>
    public MediaType(string extension, string description, string mediaType)
    {
        Extension = extension;
        Description = description;
        Type = mediaType;
    }
}
