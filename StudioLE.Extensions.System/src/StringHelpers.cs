namespace StudioLE.Extensions.System;

/// <summary>
/// Methods to help with <see cref="string"/>.
/// </summary>
public static class StringHelpers
{
    /// <summary>
    /// <inheritdoc cref="string.Join(string, IEnumerable{string})"/>
    /// </summary>
    /// <param name="this">The <see cref="string"/> to concatenate.</param>
    /// <param name="separator">The separator placed between the concatenated <see cref="string"/>.</param>
    /// <returns>The concatenated string.</returns>
    public static string Join(this IEnumerable<string> @this, string? separator = null)
    {
        separator ??= Environment.NewLine;
        return string.Join(separator, @this);
    }

    /// <summary>
    /// Replaces all "\r\n" sequences in the current string with "\n".
    /// </summary>
    /// <remarks>
    /// There is a better native implementation of this in .NET 6.0.
    /// </remarks>
    /// <seealso href="https://docs.microsoft.com/en-us/dotnet/api/system.string.replacelineendings?view=net-6.0"/>
    /// <seealso href="https://github.com/dotnet/runtime/blob/00f82acfe45b217bed2c7071828939a9f271253f/src/libraries/System.Private.CoreLib/src/System/String.Manipulation.cs#L1168-L1299"/>
    public static string ReplaceWindowsLineEndings(this string @this)
    {
        return @this.Replace("\r\n", "\n");
    }
}
