using System.Text.RegularExpressions;

namespace StudioLE.Extensions.Logging;

/// <summary>
/// Methods to help with ANSI escape codes.
/// </summary>
public static class AnsiHelpers
{
    /// <summary>
    /// Get the ANSI reset sequence.
    /// </summary>
    /// <returns>The ANSI reset sequence.</returns>
    public static string ResetSequence()
    {
        return "\x1B[0m";
    }

    /// <summary>
    /// Get the ANSI sequence to set the foreground to <paramref name="color"/>.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>The ANSI escape sequence.</returns>
    public static string GetForegroundSequence(AnsiColor color)
    {
        int code = (int)color;
        return $"\x1B[3{code}m";
    }

    /// <summary>
    /// Get the ANSI sequence to set the foreground to bright <paramref name="color"/>.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>The ANSI escape sequence.</returns>
    public static string GetBrightForegroundSequence(AnsiColor color)
    {
        int code = (int)color;
        return $"\x1B[9{code}m";
    }

    /// <summary>
    /// Get the ANSI sequence to set the background to <paramref name="color"/>.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>The ANSI escape sequence.</returns>
    public static string GetBackgroundSequence(AnsiColor color)
    {
        int code = (int)color;
        return $"\x1B[4{code}m";
    }

    /// <summary>
    /// Get the ANSI sequence to set the background to bright <paramref name="color"/>.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>The ANSI escape sequence.</returns>
    public static string GetBrightBackgroundSequence(AnsiColor color)
    {
        int code = (int)color;
        return $"\x1B[101{code}m";
    }

    /// <summary>
    /// Get the ANSI sequence to set <paramref name="style"/>.
    /// </summary>
    /// <param name="style">The style to use.</param>
    /// <returns>The ANSI escape sequence.</returns>
    public static string GetStyleSequence(AnsiStyle style)
    {
        int code = (int)style;
        return $"\x1B[{code}m";
    }

    /// <summary>
    /// Replace all ANSI escape sequences in <paramref name="input"/>.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>The input without any ANSI escape sequences.</returns>
    public static string ReplaceEscapeSequences(string input)
    {
        return Regex.Replace(input, @"\x1B\[[0-9;]+m", string.Empty);
    }
}
