using System.Text.RegularExpressions;

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
        if(IsWindowsDrivePath(absolutePath))
            return "file:///" + absolutePath.Replace('\\', '/');
        return "file://" + absolutePath.Replace('\\', '/');
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
    public static string? GetPath(string uri)
    {
        if(IsFileUriWithWindowsDrive(uri))
            return uri.Substring(8);
        if(uri.StartsWith("file:///"))
            return uri.Substring(7);
        return null;
    }

    private static bool IsFileUriWithWindowsDrive(string uri)
    {
        Regex regex = new(@"^file:///([a-zA-Z]:)\/");
        Match match = regex.Match(uri);
        return match.Success;
    }

    private static bool IsWindowsDrivePath(string path)
    {
        Regex regex = new(@"^([a-zA-Z]:)[\/\\]");
        Match match = regex.Match(path);
        return match.Success;
    }
}
