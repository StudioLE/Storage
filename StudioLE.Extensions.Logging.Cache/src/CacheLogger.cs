using Microsoft.Extensions.Logging;

namespace StudioLE.Extensions.Logging.Cache;

/// <summary>
/// An <see cref="ILogger"/> which stores a collection of all logs to review later.
/// </summary>
public class CacheLogger : ILogger
{
    private readonly string _categoryName;
    private readonly Action<LogEntry> _onLog;
    private readonly List<LogEntry> _logs = new();

    /// <summary>
    /// The logs.
    /// </summary>
    public IReadOnlyCollection<LogEntry> Logs => _logs.AsReadOnly();

    /// <summary>
    /// Create an instance of <see cref="CacheLogger"/>.
    /// </summary>
    public CacheLogger(string categoryName, Action<LogEntry> onLog)
    {
        _categoryName = categoryName;
        _onLog = onLog;
    }

    /// <inheritdoc />
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        LogEntry log = new()
        {
            Category = _categoryName,
            LogLevel = logLevel,
            EventId = eventId,
            State = typeof(TState),
            Exception = exception,
            Message = formatter.Invoke(state, exception)
        };
        _logs.Add(log);
        _onLog.Invoke(log);
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
