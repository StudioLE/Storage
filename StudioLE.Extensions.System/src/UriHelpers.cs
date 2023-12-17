namespace StudioLE.Extensions.System;

/// <summary>
/// Methods to help with <see cref="Uri"/>.
/// </summary>
public static class UriHelpers
{
    /// <summary>
    /// Get the file name of <see cref="Uri"/>.
    /// </summary>
    /// <param name="this"></param>
    /// <returns>The file name.</returns>
    /// <exception cref="Exception">Thrown if the uri has no segments.</exception>
    public static string GetFileName(this Uri @this)
    {
        return @this.Segments.LastOrDefault() ?? throw new("Failed to GetFileName. The Uri had no segments.");
    }

    /// <summary>
    /// Get the file extension of <see cref="Uri"/> including <paramref name="prefix"/>.
    /// </summary>
    /// <summary>
    /// The file extension of <see cref="Uri"/> including <paramref name="prefix"/>.
    /// </summary>
    public static string GetFileExtension(this Uri @this, string prefix = ".")
    {
        string fileName = @this.GetFileName();
        string fileExtension = fileName.Split('.').Last();
        return prefix + fileExtension;
    }
}
