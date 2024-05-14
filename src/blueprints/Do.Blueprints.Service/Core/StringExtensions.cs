namespace System;

public static partial class StringExtensions
{
    public static string Join<T>(this IEnumerable<T> enumerable, char separator) =>
        enumerable.Join($"{separator}");

    public static string Join<T>(this IEnumerable<T> enumerable,
        string? separator = default
    ) => string.Join(separator ?? string.Empty, enumerable);
}