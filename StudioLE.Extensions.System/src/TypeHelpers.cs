namespace StudioLE.Extensions.System;

/// <summary>
/// Methods to help with <see cref="Type"/>.
/// </summary>
public static class TypeHelpers
{
    /// <summary>
    /// Determines if <paramref name="this"/> has a base type of <paramref name="baseType"/>.
    /// </summary>
    /// <param name="this">The type to check.</param>
    /// <param name="baseType">The base type to look for.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="this"/> has a base type of <paramref name="baseType"/>.
    /// </returns>
    internal static bool HasBaseType(this Type @this, Type baseType)
    {
        return @this
            .GetBaseTypes()
            .Any(x => x == baseType);
    }

    /// <summary>
    /// Get all the base types of <paramref name="this"/>.
    /// </summary>
    /// <param name="this">The type to inspect.</param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of <see cref="Type"/> containing all the base types of <paramref name="this"/>.
    /// </returns>
    internal static IEnumerable<Type> GetBaseTypes(this Type @this)
    {
        while (true)
        {
            if (@this.BaseType is null)
                break;
            @this = @this.BaseType;
            yield return @this;
        }
    }
}
