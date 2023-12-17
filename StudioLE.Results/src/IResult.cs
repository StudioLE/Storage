namespace StudioLE.Results;

/// <summary>
/// A result that may include <see cref="Warnings"/> or <see cref="Errors"/>.
/// </summary>
/// <remarks>
/// DEPRECATED: It would be better practice to log warnings and errors using an <c>ILogger</c>
/// and simply return <see langword="null"/> on failure in a <c>Nullable</c> context.
/// </remarks>
public interface IResult
{
    /// <summary>
    /// The warnings that occurred producing the <see cref="IResult{T}"/>.
    /// </summary>
    public string[] Warnings { get; set; }

    /// <summary>
    /// The errors that occurred producing the <see cref="IResult{T}"/>.
    /// </summary>
    public string[] Errors { get; }
}

/// <summary>
/// A result that may produces a <typeparamref name="T"/> value with <see cref="Warnings"/> or <see cref="Errors"/>.
/// </summary>
/// <remarks>
/// DEPRECATED: It would be better practice to log warnings and errors using an <c>ILogger</c>
/// and simply return <see langword="null"/> on failure in a <c>Nullable</c> context.
/// </remarks>
public interface IResult<out T>
{
    /// <summary>
    /// The warnings that occurred producing the <see cref="IResult{T}"/>.
    /// </summary>
    public string[] Warnings { get; set; }

    /// <summary>
    /// The errors that occurred producing the <see cref="IResult{T}"/>.
    /// </summary>
    public string[] Errors { get; }
}
