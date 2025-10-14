namespace Baked;

public static class CoreExtensions
{
    public static string Join<T>(this IEnumerable<T> enumerable, char separator) =>
        enumerable.Join($"{separator}");

    public static string Join<T>(this IEnumerable<T> enumerable,
        string? separator = default
    ) => string.Join(separator ?? string.Empty, enumerable);

    public static DateTime GetNow(this TimeProvider timeProvider) =>
        timeProvider.GetLocalNow().DateTime;
}