namespace StudioLE.Results;

/// <summary>
/// Methods to help with <see cref="IResult"/> and <see cref="IResult{T}"/>.
/// </summary>
public static class ResultHelpers
{
    /// <summary>
    /// Validate the <paramref name="result"/>.
    /// Throw an exception if it is not a <see cref="Success{T}"/>.
    /// </summary>
    public static T? GetValueAsNullable<T>(this IResult<T> result) where T : class
    {
        return result is Success<T> success
            ? success.Value
            : null;
    }

    /// <summary>
    /// Get the <typeparamref name="T"/> value of the <paramref name="result"/>
    /// if it is a <see cref="Success{T}"/>.
    /// If not then throw an <see cref="Exception"/>.
    /// </summary>
    /// <param name="result">The result to evaluate.</param>
    /// <param name="contextMessage">An optional context message to be prepended to any errors on failure.</param>
    /// <typeparam name="T">The type of value to retrieve.</typeparam>
    /// <returns>The value if successful.</returns>
    /// <exception cref="Exception">Throws if not a success.</exception>
    /// <remarks>
    /// WARNING: <typeparamref name="T"/> must be an exact match. Generics matching does not support inheritance.
    /// </remarks>
    public static T GetValueOrThrow<T>(this IResult<T> result, string? contextMessage = null)
    {
        if (result is Success<T> success)
            return success;
        string message = contextMessage is null
            ? string.Empty
            : contextMessage + Environment.NewLine;
        if (result.Errors.Any())
            message += string.Join(Environment.NewLine, result.Errors) + Environment.NewLine;
        throw new(message);
    }
    /// <summary>
    /// Validate the <paramref name="result"/>.
    /// Throw an exception if it is not a <see cref="Success"/>.
    /// </summary>
    /// <param name="result">The result to evaluate.</param>
    /// <param name="contextMessage">An optional context message to be prepended to any errors on failure.</param>
    /// <exception cref="Exception">Throws if not a success.</exception>
    public static void ThrowOnFailure(this IResult result, string? contextMessage = null)
    {
        if (result is Success)
            return;
        string message = contextMessage is null
            ? string.Empty
            : contextMessage + Environment.NewLine;
        if (result.Errors.Any())
            message += string.Join(Environment.NewLine, result.Errors) + Environment.NewLine;
        throw new(message);
    }
}
