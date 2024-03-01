using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace StudioLE.Serialization.Composition;

/// <summary>
/// A branch or leaf in the composite tree representation of an <see cref="object"/>.
/// <see cref="ObjectProperty"/> is a branch therefore it has a <see cref="Parent"/>
/// and may have <see cref="Children"/>.
/// </summary>
public class ObjectProperty : IObjectComponent
{
    /// <inheritdoc/>
    public Type Type { get; }

    /// <inheritdoc/>
    public bool IsNullable { get; }

    /// <inheritdoc/>
    public IReadOnlyCollection<IObjectComponent> Children { get; }

    /// <summary>
    /// The <see cref="PropertyInfo"/> of the property.
    /// </summary>
    public PropertyInfo Property { get; }

    /// <summary>
    /// The parent of the property.
    /// </summary>
    public IObjectComponent Parent { get; }

    /// <summary>
    /// The key of the property.
    /// </summary>
    /// <remarks>
    /// The key is the name of the property.
    /// </remarks>
    public string Key { get; }

    /// <summary>
    /// The fully qualified key of the property.
    /// </summary>
    /// <remarks>
    /// The fully qualified key is the name of the property, prefixed by the fully qualified key of the parent.
    /// </remarks>
    public string FullKey { get; }

    /// <summary>
    /// The helper text of the property.
    /// </summary>
    /// <remarks>
    /// The helper text is the description of the property, or just the name if that's not set.
    /// </remarks>
    public string HelperText { get; private set; }

    internal ObjectProperty(PropertyInfo property, IObjectComponent parent)
    {
        Property = property;
        Parent = parent;
        Type? underlyingType = Nullable.GetUnderlyingType(property.PropertyType);
        IsNullable = underlyingType is not null;
        Type type = underlyingType ?? property.PropertyType;
        Type = type;
        Key = property.Name;
        FullKey = parent is ObjectProperty parentProperty
            ? $"{parentProperty.FullKey}.{Key}"
            : Key;
        // TODO: Get the HelperText from DescriptionAttribute
        HelperText = property.Name;
        Children = ObjectComponentHelpers.CreateProperties(this);
    }

    /// <inheritdoc/>
    public bool CanSet()
    {
        return Property.SetMethod is not null;
    }

    /// <inheritdoc/>
    public object? GetValue()
    {
        object? parentValue = Parent.GetValue();
        if(parentValue is null)
            return null;
        return Property.GetValue(parentValue);
    }

    /// <inheritdoc/>
    public void SetValue(object? value)
    {
        if (Property.SetMethod is null)
            throw new("Property doesn't have a setter.");
        object? parentValue = Parent.GetValue();
        Property.SetValue(parentValue, value);
        if (!Parent.Type.IsValueType)
            return;
        Parent.SetValue(parentValue);
    }

    /// <summary>
    /// Determine if the value of the property is valid according to the <see cref="ValidationAttribute"/>.
    /// </summary>
    /// <returns>
    /// A collection of errors or an empty collection if the value is valid.
    /// </returns>
    public IReadOnlyCollection<string> ValidateValue()
    {
        object? value = GetValue();
        if (value is null)
            return ["Value is null."];
        ValidationContext context = new(value)
        {
            DisplayName = FullKey
        };
        List<ValidationResult> results = new();
        ValidationAttribute[] validationAttributes = Property
            .GetCustomAttributes<ValidationAttribute>()
            .ToArray();
        if (Validator.TryValidateValue(value, context, results, validationAttributes))
            return Array.Empty<string>();
        return results
            .Select(x => x.ErrorMessage)
            .OfType<string>()
            .ToArray();
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return FullKey;
    }
}
