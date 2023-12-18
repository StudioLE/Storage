using System.Text;

namespace StudioLE.Extensions.Logging;

/// <summary>
/// A builder for ANSI escape codes.
/// </summary>
public class AnsiFormatBuilder
{
    private StringBuilder _str = new();

    /// <summary>
    /// Reset the builder.
    /// </summary>
    public void Reset()
    {
        _str = new();
    }

    /// <summary>
    /// Set the foreground to <paramref name="color"/>.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>The builder.</returns>
    public AnsiFormatBuilder WithForegroundColor(AnsiColor color)
    {
        _str.Append(AnsiHelpers.GetForegroundSequence(color));
        return this;
    }

    /// <summary>
    /// Set the foreground to bright <paramref name="color"/>.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>The builder.</returns>
    public AnsiFormatBuilder WithBrightForegroundColor(AnsiColor color)
    {
        _str.Append(AnsiHelpers.GetBrightForegroundSequence(color));
        return this;
    }

    /// <summary>
    /// Set the background to <paramref name="color"/>.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>The builder.</returns>
    public AnsiFormatBuilder WithBackgroundColor(AnsiColor color)
    {
        _str.Append(AnsiHelpers.GetBackgroundSequence(color));
        return this;
    }

    /// <summary>
    /// Set the foreground to <paramref name="color"/>.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <returns>The builder.</returns>
    public AnsiFormatBuilder WithBrightBackgroundColor(AnsiColor color)
    {
        _str.Append(AnsiHelpers.GetBrightBackgroundSequence(color));
        return this;
    }

    /// <summary>
    /// Set the style to <paramref name="style"/>.
    /// </summary>
    /// <param name="style">The color to use.</param>
    /// <returns>The builder.</returns>
    public AnsiFormatBuilder WithStyle(AnsiStyle style)
    {
        _str.Append(AnsiHelpers.GetStyleSequence(style));
        return this;
    }

    /// <summary>
    /// Create the full ANSI escape code.
    /// </summary>
    /// <returns>The ANSI escape code.</returns>
    public string Build()
    {
        return _str.ToString();
    }
}
