namespace StudioLE.Storage.Paths;

/// <summary>
/// Methods to help with file URIs.
/// </summary>
/// <see href="https://en.wikipedia.org/wiki/File_URI_scheme"/>
public static class FileUriHelpers
{
    /// <summary>
    /// Determine if the uri is a file URI.
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <returns>
    /// <see langword="true"/> if it is a file URI; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsFileUri(string uri)
    {
        return uri.StartsWith("file:/");
    }

    /// <summary>
    /// Determine if the uri is a file URI.
    /// </summary>
    /// <remarks>
    /// Network hostname paths are not supported.
    /// </remarks>
    /// <param name="uri">The URI.</param>
    /// <returns>
    /// <see langword="true"/> if it is a file URI; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool Exists(string uri)
    {
        if(!IsFileUri(uri))
            return false;
        string? path = GetPath(uri);
        return File.Exists(path);
    }

    /// <summary>
    /// Create a file URI from an absolute path.
    /// </summary>
    /// <remarks>
    /// Network hostname paths are not supported.
    /// </remarks>
    /// <param name="absolutePath"></param>
    /// <returns></returns>
    public static string FromAbsolutePath(string absolutePath)
    {
        if(IsFileUri(absolutePath))
            return absolutePath;
        return "file:///" + absolutePath.Replace('\\', '/');

    }

    /// <summary>
    /// Get the path from a file URI.
    /// </summary>
    /// <remarks>
    /// Network hostname paths are not supported.
    /// </remarks>
    /// <param name="uri">The uri.</param>
    /// <returns>
    /// The path, or <see langword="null"/> if not a file URI.
    /// </returns>
    private static string? GetPath(string uri)
    {
        if(uri.StartsWith("file:///"))
            return uri.Substring(7);
        return null;
    }
}
