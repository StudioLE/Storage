namespace StudioLE.Storage.Media;

/// <inheritdoc cref="IMediaTypeProvider"/>
public class MediaTypeProvider : IMediaTypeProvider
{
    private readonly IReadOnlyDictionary<string, MediaType> _byExtension;
    private readonly IReadOnlyDictionary<string, MediaType> _byMediaType;

    /// <summary>
    /// DI constructor for <see cref="MediaTypeProvider"/>.
    /// </summary>
    public MediaTypeProvider(IReadOnlyCollection<MediaType> mediaTypes)
    {
        _byExtension = mediaTypes
            .GroupBy(x => x.Extension)
            .ToDictionary(x => x.Key, x => x.First());
        _byMediaType = mediaTypes
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.First());
    }

    /// <summary>
    /// Get the media type corresponding to <paramref name="fileExtension"/>.
    /// </summary>
    /// <return>
    /// The media types, or <see langword="null"/> if not known.
    /// </return>
    public MediaType? ByFileExtension(string fileExtension)
    {
        if (!fileExtension.StartsWith("."))
            fileExtension += ".";
        return _byExtension.TryGetValue(fileExtension, out MediaType mediaType)
            ? mediaType
            : null;
    }

    /// <summary>
    /// Get the file extension (including . prefix) corresponding to <paramref name="mediaType"/>.
    /// </summary>
    /// <return>
    /// The file extension (including . prefix) corresponding to <paramref name="mediaType"/>,
    /// or <see langword="null"/> if not known.
    /// </return>
    public MediaType? ByMediaType(string mediaType)
    {
        return _byMediaType.TryGetValue(mediaType, out MediaType result)
            ? result
            : null;
    }
    /// <summary>
    /// Create a default instance of <see cref="MediaTypeProvider"/> with common media types.
    /// </summary>
    /// <returns>
    /// A <see cref="MediaTypeProvider"/>.
    /// </returns>
    /// <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Common_types"/>
    public static MediaTypeProvider CreateDefault()
    {
        MediaType[] mediaTypes =
        [
            new(".aac", "AAC audio", "audio/aac"),
            new(".abw", "AbiWord document", "application/x-abiword"),
            new(".apng", "Animated Portable Network Graphics (APNG) image", "image/apng"),
            new(".arc", "Archive document (multiple files embedded)", "application/x-freearc"),
            new(".avif", "AVIF image", "image/avif"),
            new(".avi", "AVI: Audio Video Interleave", "video/x-msvideo"),
            new(".azw", "Amazon Kindle eBook format", "application/vnd.amazon.ebook"),
            new(".bin", "Any kind of binary data", "application/octet-stream"),
            new(".bmp", "Windows OS/2 Bitmap Graphics", "image/bmp"),
            new(".bz", "BZip archive", "application/x-bzip"),
            new(".bz2", "BZip2 archive", "application/x-bzip2"),
            new(".cda", "CD audio", "application/x-cdf"),
            new(".csh", "C-Shell script", "application/x-csh"),
            new(".css", "Cascading Style Sheets (CSS)", "text/css"),
            new(".csv", "Comma-separated values (CSV)", "text/csv"),
            new(".doc", "Microsoft Word", "application/msword"),
            new(".docx", "Microsoft Word (OpenXML)", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"),
            new(".eot", "MS Embedded OpenType fonts", "application/vnd.ms-fontobject"),
            new(".epub", "Electronic publication (EPUB)", "application/epub+zip"),
            new(".gz", "GZip Compressed Archive", "application/gzip"),
            new(".gif", "Graphics Interchange Format (GIF)", "image/gif"),
            new(".htm", "HyperText Markup Language (HTML)", "text/html"),
            new(".html", "HyperText Markup Language (HTML)", "text/html"),
            new(".ico", "Icon format", "image/vnd.microsoft.icon"),
            new(".ics", "iCalendar format", "text/calendar"),
            new(".jar", "Java Archive (JAR)", "application/java-archive"),
            new(".jpg", "JPEG images", "image/jpeg"),
            new(".jpeg", "JPEG images", "image/jpeg"),
            new(".js", "JavaScript", "text/javascript"),
            new(".json", "JSON format", "application/json"),
            new(".jsonld", "JSON-LD format", "application/ld+json"),
            new(".mid", "Musical Instrument Digital Interface (MIDI)", "audio/midi, audio/x-midi"),
            new(".midi", "Musical Instrument Digital Interface (MIDI)", "audio/midi, audio/x-midi"),
            new(".mjs", "JavaScript module", "text/javascript"),
            new(".mp3", "MP3 audio", "audio/mpeg"),
            new(".mp4", "MP4 video", "video/mp4"),
            new(".mpeg", "MPEG Video", "video/mpeg"),
            new(".mpkg", "Apple Installer Package", "application/vnd.apple.installer+xml"),
            new(".odp", "OpenDocument presentation document", "application/vnd.oasis.opendocument.presentation"),
            new(".ods", "OpenDocument spreadsheet document", "application/vnd.oasis.opendocument.spreadsheet"),
            new(".odt", "OpenDocument text document", "application/vnd.oasis.opendocument.text"),
            new(".oga", "OGG audio", "audio/ogg"),
            new(".ogv", "OGG video", "video/ogg"),
            new(".ogx", "OGG", "application/ogg"),
            new(".opus", "Opus audio", "audio/opus"),
            new(".otf", "OpenType font", "font/otf"),
            new(".png", "Portable Network Graphics", "image/png"),
            new(".pdf", "Adobe Portable Document Format (PDF)", "application/pdf"),
            new(".php", "Hypertext Preprocessor (Personal Home Page)", "application/x-httpd-php"),
            new(".ppt", "Microsoft PowerPoint", "application/vnd.ms-powerpoint"),
            new(".pptx", "Microsoft PowerPoint (OpenXML)", "application/vnd.openxmlformats-officedocument.presentationml.presentation"),
            new(".rar", "RAR archive", "application/vnd.rar"),
            new(".rtf", "Rich Text Format (RTF)", "application/rtf"),
            new(".sh", "Bourne shell script", "application/x-sh"),
            new(".svg", "Scalable Vector Graphics (SVG)", "image/svg+xml"),
            new(".tar", "Tape Archive (TAR)", "application/x-tar"),
            new(".tif", "Tagged Image File Format (TIFF)", "image/tiff"),
            new(".tiff", "Tagged Image File Format (TIFF)", "image/tiff"),
            new(".ts", "MPEG transport stream", "video/mp2t"),
            new(".ttf", "TrueType Font", "font/ttf"),
            new(".txt", "Text, (generally ASCII or ISO 8859-n)", "text/plain"),
            new(".vsd", "Microsoft Visio", "application/vnd.visio"),
            new(".wav", "Waveform Audio Format", "audio/wav"),
            new(".weba", "WEBM audio", "audio/webm"),
            new(".webm", "WEBM video", "video/webm"),
            new(".webp", "WEBP image", "image/webp"),
            new(".woff", "Web Open Font Format (WOFF)", "font/woff"),
            new(".woff2", "Web Open Font Format (WOFF)", "font/woff2"),
            new(".xhtml", "XHTML", "application/xhtml+xml"),
            new(".xls", "Microsoft Excel", "application/vnd.ms-excel"),
            new(".xlsx", "Microsoft Excel (OpenXML)", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"),
            new(".xml", "XML", "application/xml is recommended as of RFC 7303 (section 4.1), but text/xml is still used sometimes. You can assign a specific MIME type to a file with .xml extension depending on how its contents are meant to be interpreted. For instance, an Atom feed is application/atom+xml, but application/xml serves as a valid default."),
            new(".xul", "XUL", "application/vnd.mozilla.xul+xml"),
            new(".zip", "ZIP archive", "application/zip"),
            new(".3gp", "3GPP audio/video container", "video/3gpp"),
            new(".3gp", "3GPP audio/video container", "audio/3gpp"),
            new(".3g2", "3GPP2 audio/video container", "video/3gpp2"),
            new(".3g2", "3GPP2 audio/video container", "audio/3gpp2"),
            new(".7z", "7-zip archive", "application/x-7z-compressed")
        ];
        return new(mediaTypes);
    }
}
