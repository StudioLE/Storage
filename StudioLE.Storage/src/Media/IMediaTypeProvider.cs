namespace StudioLE.Storage.Media;

/// <summary>
/// Resolve a media type from a file extension or a media type string.
/// </summary>
/// <seealso href="https://en.wikipedia.org/wiki/Media_type"/>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Glossary/MIME_type"/>
/// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Type"/>
public interface IMediaTypeProvider
{
    /// <summary>
    /// Get the media type corresponding to <paramref name="fileExtension"/>.
    /// </summary>
    /// <return>
    /// The media types, or <see langword="null"/> if not known.
    /// </return>
    MediaType? ByFileExtension(string fileExtension);

    /// <summary>
    /// Get the file extension (including . prefix) corresponding to <paramref name="mediaType"/>.
    /// </summary>
    /// <return>
    /// The file extension (including . prefix) corresponding to <paramref name="mediaType"/>,
    /// or <see langword="null"/> if not known.
    /// </return>
    MediaType? ByMediaType(string mediaType);
}
