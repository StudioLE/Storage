using Microsoft.Extensions.Options;

namespace StudioLE.Storage.Files;

/// <summary>
/// An <see cref="HttpClient"/> for <see cref="HttpFileReader"/>.
/// </summary>
public class HttpFileSystemClient : HttpClient
{
    /// <summary>
    /// DI constructor for <see cref="HttpFileSystemClient"/>.
    /// </summary>
    public HttpFileSystemClient(IOptions<HttpFileSystemOptions> options)
    {
        if(string.IsNullOrEmpty(options.Value.BaseAddress))
            throw new($"The {nameof(HttpFileSystemOptions)}.{nameof(HttpFileSystemOptions.BaseAddress)} must be set.");
        BaseAddress = new(options.Value.BaseAddress);
    }
}
