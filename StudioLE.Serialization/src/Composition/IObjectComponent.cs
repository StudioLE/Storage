namespace StudioLE.Serialization.Composition;

/// <summary>
/// A composite tree representation of an <see cref="object"/>.
/// </summary>
/// <remarks>
/// Follows a <see href="https://refactoring.guru/design-patterns/composite">composite design pattern</see>.
/// </remarks>
public interface IObjectComponent
{
    /// <summary>
    /// The type of the <see cref="object"/>, or the underlying type if the type is nullable.
    /// </summary>
    public Type Type { get; }

    /// <summary>
    /// Is the type of the <see cref="object"/> nullable?
    /// </summary>
    public bool IsNullable { get; }

    /// <summary>
    /// The properties of the <see cref="object"/>.
    /// </summary>
    public IReadOnlyCollection<IObjectComponent> Children { get; }

    /// <summary>
    /// Get the current value of the <see cref="object"/>.
    /// </summary>
    public object? GetValue();

    /// <summary>
    /// Set the value of the <see cref="object"/>.
    /// </summary>
    /// <remarks>
    /// If the parent is a value type, the parent will be recursively set to the new value.
    /// </remarks>
    /// <param name="value">The value to set.</param>
    public void SetValue(object? value);

    /// <summary>
    /// Can the <see cref="object"/> value be set?
    /// </summary>
    public bool CanSet();
}
