using System.Text;

namespace StudioLE.Storage.Paths;

/// <summary>
/// Methods to help with data URIs.
/// </summary>
/// <see href="https://en.wikipedia.org/wiki/Data_URI_scheme"/>
public static class DataUriHelpers
{

    /// <summary>
    /// Determine if the file includes inline data encoded as a data URI.
    /// </summary>
    /// <param name="uri">The data URI.</param>
    /// <returns>
    /// <see langword="true"/> if the file location is a data uri; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsDataUri(string uri)
    {
        return uri.StartsWith("data:");
    }

    /// <summary>
    /// Get the string content of a data URI.
    /// </summary>
    /// <param name="uri">The data URI.</param>
    /// <returns>
    /// The content of the data URI, or an empty string if not a data URI.
    /// </returns>
    public static string GetString(string uri)
    {
        if (!IsDataUri(uri))
            return string.Empty;
        string[] parts = uri.Split(',');
        string metadata = parts[0];
        string data = string.Join("", parts.Skip(1));
        if (!metadata.Contains(";base64"))
            return data;
        return Encoding.UTF8.GetString(Convert.FromBase64String(data));
    }

    /// <summary>
    /// Get the content of a data URI as a byte array.
    /// </summary>
    /// <param name="uri">The data URI.</param>
    /// <returns>
    /// <see langword="true"/> if the file location is a data uri; otherwise, <see langword="false"/>.
    /// </returns>
    public static byte[] GetBytes(string uri)
    {
        if (!IsDataUri(uri))
            return Array.Empty<byte>();
        string[] parts = uri.Split(',');
        string metadata = parts[0];
        string data = string.Join("", parts.Skip(1));
        if (!metadata.Contains(";base64"))
            return Encoding.UTF8.GetBytes(data);;
        return Convert.FromBase64String(data);
    }
}
