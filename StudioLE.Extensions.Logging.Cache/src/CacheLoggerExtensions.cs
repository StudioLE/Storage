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
    public static ILoggingBuilder AddCache(this ILoggingBuilder builder)
    {
        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, CacheLoggerProvider>());
        return builder;
    }

    /// <summary>
    /// Get the logs from <see cref="CacheLoggerProvider"/>.
    /// </summary>
    /// <param name="services">The service provider.</param>
    /// <returns>The logs.</returns>
    /// <exception cref="Exception">Thrown if there is no <see cref="CacheLoggerProvider"/>.</exception>
    public static IReadOnlyCollection<LogEntry> GetCachedLogs(this IServiceProvider services)
    {
        CacheLoggerProvider provider = services
                                          .GetServices<ILoggerProvider>()
                                          .OfType<CacheLoggerProvider>()
                                          .FirstOrDefault()
                                      ?? throw new($"Failed to get {nameof(CacheLoggerProvider)}.");
        return provider.Logs;
    }
}
