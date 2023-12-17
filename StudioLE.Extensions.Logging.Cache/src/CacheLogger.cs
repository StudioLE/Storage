using Microsoft.Extensions.Logging;

namespace StudioLE.Extensions.Logging.Cache;

/// <summary>
/// An <see cref="ILogger"/> which stores a collection of all logs to review later.
/// </summary>
public class CacheLogger : ILogger
{
    private readonly string _categoryName;
    private readonly Action<LogEntry> _onLog;

    internal CacheLogger(string categoryName, Action<LogEntry> onLog)
    {
        _categoryName = categoryName;
        _onLog = onLog;
    }

    /// <inheritdoc />
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        _onLog.Invoke(new()
        {
            Category = _categoryName,
            LogLevel = logLevel,
            EventId = eventId,
            State = typeof(TState),
            Exception = exception,
            Message = formatter.Invoke(state, exception)
        });
    }

    /// <inheritdoc />
    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    /// <inheritdoc />
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return default;
    }
}
