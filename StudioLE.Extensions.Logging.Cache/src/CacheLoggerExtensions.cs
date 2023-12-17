using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace StudioLE.Extensions.Logging.Cache;

/// <summary>
/// Methods to help with <see cref="CacheLogger"/>.
/// </summary>
public static class CacheLoggerExtensions
{
    /// <summary>
    /// Add a <see cref="CacheLogger"/>.
    /// </summary>
    public static ILoggingBuilder AddTestLogger(this ILoggingBuilder builder)
    {
        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, CacheLoggerProvider>());
        return builder;
    }

    public static IReadOnlyCollection<LogEntry> GetTestLogs(this IServiceProvider services)
    {
        CacheLoggerProvider provider = services
                                          .GetServices<ILoggerProvider>()
                                          .OfType<CacheLoggerProvider>()
                                          .FirstOrDefault()
                                      ?? throw new($"Failed to get {nameof(CacheLoggerProvider)}.");
        return provider.Logs;
    }
}
