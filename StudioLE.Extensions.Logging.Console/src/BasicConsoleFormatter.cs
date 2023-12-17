using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace StudioLE.Extensions.Logging.Console;

/// <summary>
/// A basic console formatter that writes a single line message to the console without scope.
/// </summary>
/// <seealso href="https://learn.microsoft.com/en-us/dotnet/core/extensions/console-log-formatter#implement-a-custom-formatter"/>
public sealed class BasicConsoleFormatter : ConsoleFormatter, IDisposable
{
    public const string FormatterName = "basic";
    private readonly IDisposable? _optionsReloadToken;
    private BasicConsoleFormatterOptions _formatterOptions;

    public BasicConsoleFormatter(IOptionsMonitor<BasicConsoleFormatterOptions> options) : base(FormatterName)
    {
        (_optionsReloadToken, _formatterOptions) = (options.OnChange(ReloadLoggerOptions), options.CurrentValue);
    }

    private void ReloadLoggerOptions(BasicConsoleFormatterOptions options)
    {
        _formatterOptions = options;
    }

    public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
    {
        string? message =
            logEntry.Formatter.Invoke(logEntry.State, logEntry.Exception);
        if (message is null)
            return;
        if (_formatterOptions.DisableColors)
        {
            textWriter.WriteLine(message);
            return;
        }
        ConsoleColor? foregroundColor = GetForegroundColor(logEntry.LogLevel);
        string foregroundColorAnsi = AnsiHelpers.GetForegroundColorEscapeCode(foregroundColor);
        textWriter.Write(foregroundColorAnsi);
        ConsoleColor? backgroundColor = GetBackgroundColor(logEntry.LogLevel);
        string backgroundColorAnsi = AnsiHelpers.GetBackgroundColorEscapeCode(backgroundColor);
        textWriter.Write(backgroundColorAnsi);
        textWriter.Write(message);
        textWriter.Write(AnsiHelpers.DefaultForegroundColor);
        textWriter.Write(AnsiHelpers.DefaultBackgroundColor);
        textWriter.WriteLine();
    }

    public void Dispose()
    {
        _optionsReloadToken?.Dispose();
    }

    public ConsoleColor? GetForegroundColor(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Trace => ConsoleColor.DarkGray,
            LogLevel.Debug => ConsoleColor.DarkGray,
            LogLevel.Information => ConsoleColor.Green,
            LogLevel.Warning => ConsoleColor.Yellow,
            LogLevel.Error => ConsoleColor.Red,
            LogLevel.Critical => ConsoleColor.Black,
            LogLevel.None => ConsoleColor.DarkMagenta,
            _ => null
        };
    }

    private static ConsoleColor? GetBackgroundColor(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Critical => ConsoleColor.DarkRed,
            LogLevel.None => ConsoleColor.DarkMagenta,
            _ => null
        };
    }
}
