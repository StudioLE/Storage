using Microsoft.Extensions.Logging;

namespace StudioLE.Extensions.Logging.Cache;

/// <summary>
/// A factory to create <see cref="CacheLogger"/>.
/// </summary>
[ProviderAlias("Test")]
public class CacheLoggerProvider : ILoggerProvider
{
    private readonly List<LogEntry> _logs = new();

    /// <summary>
    /// The logs.
    /// </summary>
    public IReadOnlyCollection<LogEntry> Logs => _logs.AsReadOnly();

    /// <inheritdoc />
    public ILogger CreateLogger(string categoryName)
    {
        return new CacheLogger(categoryName, _logs.Add);
    }

    /// <inheritdoc />
    public void Dispose()
    {
    }
}
