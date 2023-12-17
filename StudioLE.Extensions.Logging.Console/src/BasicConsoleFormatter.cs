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
        string ansiFormat = GetAnsiFormat(logEntry.LogLevel);
        textWriter.Write(AnsiHelpers.ResetSequence());
        textWriter.Write(ansiFormat);
        textWriter.Write(message);
        textWriter.Write(AnsiHelpers.ResetSequence());
        textWriter.WriteLine();
    }

    public void Dispose()
    {
        _optionsReloadToken?.Dispose();
    }

    private static string GetAnsiFormat(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Trace => new AnsiFormatBuilder()
                .WithStyle(AnsiStyle.Dim)
                .Build(),
            LogLevel.Debug => new AnsiFormatBuilder()
                .WithStyle(AnsiStyle.Dim)
                .Build(),
            LogLevel.Information => new AnsiFormatBuilder()
                .WithBrightForegroundColor(AnsiColor.White)
                .Build(),
            LogLevel.Warning => new AnsiFormatBuilder()
                .WithForegroundColor(AnsiColor.Yellow)
                .Build(),
            LogLevel.Error => new AnsiFormatBuilder()
                .WithForegroundColor(AnsiColor.Red)
                .Build(),
            LogLevel.Critical => new AnsiFormatBuilder()
                .WithForegroundColor(AnsiColor.Black)
                .WithBackgroundColor(AnsiColor.Red)
                .Build(),
            LogLevel.None => new AnsiFormatBuilder()
                .WithBackgroundColor(AnsiColor.Magenta)
                .Build(),
            (LogLevel)10 => new AnsiFormatBuilder()
                .WithForegroundColor(AnsiColor.Cyan)
                .Build(),
            _ => string.Empty
        };
    }
}
