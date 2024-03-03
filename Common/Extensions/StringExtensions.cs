namespace DistanceCalculator.Common.Extensions;

public static class StringExtensions
{
    public static bool IsEmpty(this string? val)
    {
        return string.IsNullOrWhiteSpace(val);
    }
}
