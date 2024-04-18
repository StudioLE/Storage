namespace StudioLE.Storage.Media;

/// <summary>
/// Methods to help with <see cref="MediaType"/> and <see cref="MediaTypeProvider"/>.
/// </summary>
public static class MediaTypeHelpers
{
    /// <summary>
    /// Get the media type first media type as a string corresponding to <paramref name="fileExtension"/>.
    /// </summary>
    /// <return>
    /// The first media type as a string, or <see langword="null"/> if not known.
    /// </return>
    public static string? StringByFileExtension(this IMediaTypeProvider @this, string fileExtension)
    {
        return @this
            .ByFileExtension(fileExtension)
            ?.Type;
    }

    /// <summary>
    /// Get the file extension (including . prefix) corresponding to <paramref name="mediaType"/>.
    /// </summary>
    /// <return>
    /// The file extension (including . prefix) corresponding to <paramref name="mediaType"/>,
    /// or <see langword="null"/> if not known.
    /// </return>
    public static string? FileExtensionByMediaType(this IMediaTypeProvider @this, string mediaType)
    {
        return @this
            .ByMediaType(mediaType)
            ?.Extension;
    }
}
