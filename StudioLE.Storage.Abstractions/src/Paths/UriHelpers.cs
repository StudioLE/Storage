using System.Text.RegularExpressions;

namespace StudioLE.Storage.Paths;

/// <summary>
/// Methods to help with URIs.
/// </summary>
public static class UriHelpers
{
    /// <summary>
    /// Get the file extension (including . prefix) from a URI.
    /// </summary>
    /// <param name="uri">The uri.</param>
    /// <returns>
    /// The file extension (including . prefix)
    /// </returns>
    public static string? GetFileExtension(string uri)
    {
        Regex regex = new(@"\.[a-zA-Z0-9]+$");
        Match match = regex.Match(uri);
        return match.Success
            ? match.Value
            : null;
    }
}
