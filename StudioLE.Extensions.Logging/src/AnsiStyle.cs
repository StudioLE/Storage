namespace StudioLE.Extensions.Logging;

/// <summary>
/// ANSI style for console output.
/// </summary>
public enum AnsiStyle
{
    /// <summary>
    /// No style.
    /// </summary>
    None = 0,

    /// <summary>
    /// Bold.
    /// </summary>
    Bold = 1,

    /// <summary>
    /// Dim.
    /// </summary>
    Dim = 2,

    /// <summary>
    /// Italic.
    /// </summary>
    Italic = 3,

    /// <summary>
    /// Underlined.
    /// </summary>
    Underline = 4,

    /// <summary>
    /// Blinking.
    /// </summary>
    Blink = 5,

    /// <summary>
    /// Inverted.
    /// </summary>
    Invert = 7,

    /// <summary>
    /// Hidden
    /// </summary>
    Hidden = 8,

    /// <summary>
    /// Strike-through.
    /// </summary>
    Strike = 9
}
