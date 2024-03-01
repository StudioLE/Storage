namespace StudioLE.Serialization.Composition;

/// <summary>
/// A composite tree representation of an <see cref="object"/>.
/// <see cref="ObjectTree"/> is the start of the tree, therefore it has no parent.
/// </summary>
/// <remarks>
/// Follows a <see href="https://refactoring.guru/design-patterns/composite">composite design pattern</see>.
/// </remarks>
public class ObjectTree : IObjectComponent
{
    private object? _value;

    /// <inheritdoc/>
    public Type Type { get; }

    /// <inheritdoc/>
    public bool IsNullable { get; }

    /// <inheritdoc/>
    public IReadOnlyCollection<IObjectComponent> Children { get; }

    /// <summary>
    /// Creates a new instance of <see cref="ObjectTree"/>.
    /// </summary>
    public ObjectTree(object value)
    {
        _value = value;
        Type type = value.GetType();
        Type? underlyingType = Nullable.GetUnderlyingType(type);
        IsNullable = underlyingType is not null;
        Type = underlyingType ?? type;
        Children = ObjectComponentHelpers.CreateProperties(this);
    }

    /// <inheritdoc/>
    public object? GetValue()
    {
        return _value;
    }

    /// <inheritdoc/>
    public void SetValue(object? value)
    {
        _value = value;
    }

    /// <inheritdoc/>
    public bool CanSet()
    {
        return true;
    }
}
