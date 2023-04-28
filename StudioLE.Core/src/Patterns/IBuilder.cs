namespace StudioLE.Core.Patterns;

/// <summary>
/// Build a <typeparamref name="TResult"/> using a <see href="https://refactoring.guru/design-patterns/builder">builder pattern</see>.
/// </summary>
public interface IBuilder<out TResult>
{
    /// <inheritdoc cref="IBuilder{T}"/>
    public TResult Build();
}
