namespace StudioLE.Results;

/// <summary>
/// A successful <see cref="IResult"/>
/// that may still contain <see cref="Errors"/> or <see cref="Warnings"/>.
/// </summary>
public class Success : IResult
{
    /// <inheritdoc/>
    public string[] Warnings { get; set; } = Array.Empty<string>();

    /// <inheritdoc/>
    public string[] Errors { get; } = Array.Empty<string>();
}

/// <summary>
/// A successful <see cref="IResult"/> that has a <see cref="Value"/>
/// and may still contain <see cref="Errors"/> or <see cref="Warnings"/>.
/// </summary>
public class Success<T> : IResult<T>
{
    /// <summary>
    /// The value.
    /// </summary>
    public T Value { get; }

    /// <inheritdoc/>
    public string[] Warnings { get; set; } = Array.Empty<string>();

    /// <inheritdoc/>
    public string[] Errors { get; } = Array.Empty<string>();

    /// <summary>
    /// Create a new instance of <see cref="Success{T}"/> with the specified <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    public Success(T value)
    {
        Value = value;
    }

    /// <summary>
    /// Implicitly get the <see cref="Value"/>.
    /// </summary>
    /// <returns>The value.</returns>
    public static implicit operator T(Success<T> @this)
    {
        return @this.Value;
    }
}
