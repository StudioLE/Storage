namespace StudioLE.Results;

/// <summary>
/// A failed <see cref="IResult"/> that has <see cref="Errors"/>
/// and may include <see cref="Warnings"/> or an <see cref="Exception"/>.
/// </summary>
public class Failure : IResult
{
    /// <inheritdoc />
    public string[] Warnings { get; set; } = Array.Empty<string>();

    /// <inheritdoc />
    public string[] Errors { get; }

    /// <summary>
    /// An exception that occurred producing the <see cref="IResult"/>.
    /// </summary>
    public Exception? Exception { get; }


    /// <summary>
    /// Creates a new instance of <see cref="Failure"/>.
    /// </summary>
    [Obsolete("Failure should contain at least one error.")]
    public Failure()
    {
        Errors = new[] { "An unspecified error occurred." };
    }

    /// <summary>
    /// Creates a new instance of <see cref="Failure"/> with the specified <paramref name="errors"/>.
    /// </summary>
    public Failure(params string[] errors)
    {
        Errors = errors.Any()
            ? errors
            : new[] { "An unspecified error occurred." };
    }

    /// <summary>
    /// Creates a new instance of <see cref="Failure"/> with the specified <paramref name="error"/> and <paramref name="errors"/>.
    /// </summary>
    public Failure(string error, params string[] errors)
    {
        Errors = errors.Prepend(error).ToArray();
    }

    /// <summary>
    /// Creates a new instance of <see cref="Failure"/> from an existing <paramref name="result"/> with the specified <paramref name="error"/>.
    /// </summary>
    /// <remarks>
    /// The <paramref name="error"/> is prepended to the errors of <paramref name="result"/>.
    /// </remarks>
    public Failure(IResult<object> result, params string[] error)
    {
        Errors = error
            .Concat(result.Errors)
            .ToArray();
        Warnings = result.Warnings;
    }

    /// <summary>
    /// Creates a new instance of <see cref="Failure"/> with the specified <paramref name="exception"/> and <paramref name="errors"/>.
    /// </summary>
    public Failure(Exception exception, params string[] errors)
    {
        Exception = exception;
        Errors = errors.Prepend(exception.Message).ToArray();
    }

    /// <summary>
    /// Creates a new instance of <see cref="Failure"/> with the specified <paramref name="error"/> and <paramref name="exception"/>.
    /// </summary>
    public Failure(string error, Exception exception)
    {
        Exception = exception;
        Errors = new[] { error, exception.Message };
    }
}

/// <summary>
/// A failed <see cref="IResult"/> that has <see cref="Errors"/>
/// and may include <see cref="Warnings"/> or an <see cref="Exception"/>.
/// </summary>
public class Failure<T> : IResult<T>
{
    /// <inheritdoc />
    public string[] Warnings { get; set; } = Array.Empty<string>();

    /// <inheritdoc />
    public string[] Errors { get; }

    /// <summary>
    /// An exception that occurred producing the <see cref="IResult"/>.
    /// </summary>
    public Exception? Exception { get; }

    /// <summary>
    /// Creates a new instance of <see cref="Failure{T}"/>.
    /// </summary>
    [Obsolete("Failure should contain at least one error.")]
    public Failure()
    {
        Errors = new[] { "An unspecified error occurred." };
    }

    /// <summary>
    /// Creates a new instance of <see cref="Failure{T}"/> with the specified <paramref name="errors"/>.
    /// </summary>
    public Failure(params string[] errors)
    {
        if (!errors.Any())
            throw new("Failure must contain at least one error.");
        Errors = errors;
    }

    /// <summary>
    /// Creates a new instance of <see cref="Failure{T}"/> with the specified <paramref name="error"/> and <paramref name="errors"/>.
    /// </summary>
    public Failure(string error, params string[] errors)
    {
        Errors = errors.Prepend(error).ToArray();
    }

    /// <summary>
    /// Creates a new instance of <see cref="Failure{T}"/> from an existing <paramref name="result"/> with the specified <paramref name="error"/>.
    /// </summary>
    /// <remarks>
    /// The <paramref name="error"/> is prepended to the errors of <paramref name="result"/>.
    /// </remarks>
    public Failure(IResult<object> result, params string[] error)
    {
        Errors = error
            .Concat(result.Errors)
            .ToArray();
        Warnings = result.Warnings;
    }

    /// <summary>
    /// Creates a new instance of <see cref="Failure{T}"/> with the specified <paramref name="exception"/> and <paramref name="errors"/>.
    /// </summary>
    public Failure(Exception exception, params string[] errors)
    {
        Exception = exception;
        Errors = errors.Prepend(exception.Message).ToArray();
    }

    /// <summary>
    /// Creates a new instance of <see cref="Failure{T}"/> with the specified <paramref name="error"/> and <paramref name="exception"/>.
    /// </summary>
    public Failure(string error, Exception exception)
    {
        Exception = exception;
        Errors = new[] { error, exception.Message };
    }
}
