using StudioLE.Storage.Paths;

namespace StudioLE.Storage.Files;

/// <summary>
/// Methods to help with <see cref="IFileInfo"/>.
/// </summary>
public static class FileInfoHelpers
{
    /// <summary>
    /// Determine if the file includes inline data encoded as a data URI.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <returns>
    /// <see langword="true"/> if the file location is a data uri; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsInlineData(this IFileInfo file)
    {
        return DataUriHelpers.IsDataUri(file.Location);
    }

    /// <summary>
    /// Determine if the file is a local file.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <returns>
    /// <see langword="true"/> if the file location is a data uri; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsPhysicalFile(this IFileInfo file)
    {
        return FileUriHelpers.IsFileUri(file.Location);
    }

    /// <summary>
    /// Get the inline data from the file's data URI.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <returns>
    /// <see langword="true"/> if the file location is a data uri; otherwise, <see langword="false"/>.
    /// </returns>
    public static string GetInlineData(this IFileInfo file)
    {
        return DataUriHelpers.GetString(file.Location);
    }

    /// <summary>
    /// Get the inline data from the file's data URI.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <returns>
    /// <see langword="true"/> if the file location is a data uri; otherwise, <see langword="false"/>.
    /// </returns>
    public static byte[] GetInlineDataBytes(this IFileInfo file)
    {
        return DataUriHelpers.GetBytes(file.Location);
    }
}
