namespace StudioLE.Extensions.System;

/// <summary>
/// Methods to help with <see cref="Math"/>.
/// </summary>
public static class MathHelpers
{
    /// <summary>
    /// Return an enumerable which is the cumulative sum of each preceding value in <paramref name="sequence"/>.
    /// </summary>
    /// <seealso href="https://stackoverflow.com/a/4831908/247218">Source</seealso>
    public static IEnumerable<double> CumulativeSum(this IEnumerable<double> sequence)
    {
        double sum = 0;
        foreach (double item in sequence)
        {
            sum += item;
            yield return sum;
        }
    }

    /// <summary>
    /// Round <paramref name="this"/> to <paramref name="decimalPlaces"/> using <paramref name="rounding"/>.
    /// </summary>
    /// <remarks>
    /// This differs from <see cref="Math.Round(double)"/> because it defaults to <see cref="MidpointRounding.AwayFromZero"/>.
    /// </remarks>
    public static double Round(this double @this, int decimalPlaces, MidpointRounding rounding = MidpointRounding.AwayFromZero)
    {
        double result = Math.Round(@this, decimalPlaces, rounding);
        return result == 0
            ? 0
            : result;
    }

    /// <summary>
    /// Round <paramref name="this"/> to an integer using <paramref name="rounding"/>.
    /// </summary>
    /// <remarks>
    /// This differs from <see cref="Math.Round(double)"/> because it defaults to <see cref="MidpointRounding.AwayFromZero"/>.
    /// </remarks>
    public static int RoundToInt(this double @this, MidpointRounding rounding = MidpointRounding.AwayFromZero)
    {
        return (int)Math.Round(@this, 0, rounding);
    }

    /// <inheritdoc cref="Math.Ceiling(double)"/>
    public static int CeilingToInt(this double @this)
    {
        return (int)Math.Ceiling(@this);
    }

    /// <inheritdoc cref="Math.Floor(double)"/>
    public static int FloorToInt(this double @this)
    {
        return (int)Math.Floor(@this);
    }
}
