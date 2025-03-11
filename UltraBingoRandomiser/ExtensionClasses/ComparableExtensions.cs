namespace UltrakillRaceGenerator.ExtensionClasses;

/// <summary>
/// Represents the type of range. Ends are closed by default.
/// </summary>
[Flags]
internal enum RangeType
{
    /// <summary>
    /// Both ends are excluded from the range.
    /// </summary>
    Closed = 0b00,

    /// <summary>
    /// Lower end is included in the range.
    /// </summary>
    LowerOpen = 0b01,

    /// <summary>
    /// Upper end is included in the range.
    /// </summary>
    UpperOpen = 0b10,

    /// <summary>
    /// Both ends are included in the range.
    /// </summary>
    Open = LowerOpen | UpperOpen
}

internal static class ComparableExtensions
{
    /// <summary>
    /// Determines whether the value is between the lower and upper bounds,
    /// where rangeType specifies whether the ends are inclusive or not.
    /// </summary>
    /// <typeparam name="T">The type of the value and bounds, must implement <see cref="IComparable&lt;T&gt;"/>.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="lower">The lower bound of the range.</param>
    /// <param name="upper">The upper bound of the range.</param>
    /// <param name="rangeType">Specifies whether the ends of the range are inclusive or exclusive.</param>
    /// <returns><see langword="true"/> if the value is within the specified range; otherwise, <see langword="false"/>.</returns>
    internal static bool IsBetween<T>(this T value, T lower, T upper, RangeType rangeType = RangeType.Open) where T : IComparable
    {
        bool lowerCheck = rangeType.HasFlag(RangeType.LowerOpen) ? 0 <= value.CompareTo(lower) : 0 < value.CompareTo(lower);
        bool upperCheck = rangeType.HasFlag(RangeType.UpperOpen) ? value.CompareTo(upper) <= 0 : value.CompareTo(upper) < 0;

        return lowerCheck && upperCheck;
    }
}
