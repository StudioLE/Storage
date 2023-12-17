// Parts of this file are used under the MIT license from .NET Foundation
// https://github.com/dotnet/runtime/blob/v8.0.0/src/libraries/Microsoft.Extensions.Logging.Console/src/AnsiParser.cs

namespace StudioLE.Extensions.Logging.Console;

/// <summary>
/// Methods to help with ANSI escape codes.
/// </summary>
internal static  class AnsiHelpers
{
    public const string DefaultForegroundColor = "\x1B[39m\x1B[22m";
    public const string DefaultBackgroundColor = "\x1B[49m";

    /// <summary>
    /// Get the ANSI escape code for <paramref name="color"/> as a foreground color.
    /// </summary>
    /// <param name="color">The color to evaluate.</param>
    /// <returns>The ANSI escape code.</returns>
    /// <seealso href="https://github.com/dotnet/runtime/blob/5535e31a712343a63f5d7d796cd874e563e5ac14/src/libraries/Microsoft.Extensions.Logging.Console/src/AnsiParser.cs#L133-L154"/>
    public static string GetForegroundColorEscapeCode(ConsoleColor? color)
    {
        return color switch
        {
            ConsoleColor.Black => "\x1B[30m",
            ConsoleColor.DarkRed => "\x1B[31m",
            ConsoleColor.DarkGreen => "\x1B[32m",
            ConsoleColor.DarkYellow => "\x1B[33m",
            ConsoleColor.DarkBlue => "\x1B[34m",
            ConsoleColor.DarkMagenta => "\x1B[35m",
            ConsoleColor.DarkCyan => "\x1B[36m",
            ConsoleColor.Gray => "\x1B[37m",
            ConsoleColor.Red => "\x1B[1m\x1B[31m",
            ConsoleColor.Green => "\x1B[1m\x1B[32m",
            ConsoleColor.Yellow => "\x1B[1m\x1B[33m",
            ConsoleColor.Blue => "\x1B[1m\x1B[34m",
            ConsoleColor.Magenta => "\x1B[1m\x1B[35m",
            ConsoleColor.Cyan => "\x1B[1m\x1B[36m",
            ConsoleColor.White => "\x1B[1m\x1B[37m",
            _ => DefaultForegroundColor // default foreground color
        };
    }

    /// <summary>
    /// Get the ANSI escape code for <paramref name="color"/> as a background color.
    /// </summary>
    /// <param name="color">The color to evaluate.</param>
    /// <returns>The ANSI escape code.</returns>
    /// <seealso href="https://github.com/dotnet/runtime/blob/5535e31a712343a63f5d7d796cd874e563e5ac14/src/libraries/Microsoft.Extensions.Logging.Console/src/AnsiParser.cs#L156-L170"/>
    public static string GetBackgroundColorEscapeCode(ConsoleColor? color)
    {
        return color switch
        {
            ConsoleColor.Black => "\x1B[40m",
            ConsoleColor.DarkRed => "\x1B[41m",
            ConsoleColor.DarkGreen => "\x1B[42m",
            ConsoleColor.DarkYellow => "\x1B[43m",
            ConsoleColor.DarkBlue => "\x1B[44m",
            ConsoleColor.DarkMagenta => "\x1B[45m",
            ConsoleColor.DarkCyan => "\x1B[46m",
            ConsoleColor.Gray => "\x1B[47m",
            _ => DefaultBackgroundColor // Use default background color
        };
    }
}
