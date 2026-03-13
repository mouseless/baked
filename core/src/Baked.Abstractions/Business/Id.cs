using System.Diagnostics.CodeAnalysis;

namespace Baked.Business;

public record struct Id : IParsable<Id>, IEquatable<Id>, IComparable, IComparable<Id>
{
    public static Id Empty = new(string.Empty);

    public static Id NewId() =>
        Create(Guid.NewGuid());

    public static Id Create(object value) =>
        Parse($"{value}");

    public static Id Parse(string s) =>
        Parse(s, null);

    public static Id Parse(string s, IFormatProvider? _) =>
        new(s.Trim());

    public static bool TryParse(
        [NotNullWhen(true)] string? code,
        [MaybeNullWhen(false)] out Id result
    ) => TryParse(code, null, out result);

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out Id result
    )
    {
        result = s is null ? Empty : Create(s);

        return true;
    }

    readonly string _value;

    public readonly bool IsEmpty => _value == string.Empty;

    Id(string value)
    {
        _value = value;
    }

    public readonly int CompareTo(Id other) =>
        _value.CompareTo(other._value);

    public readonly int CompareTo(object? obj)
    {
        if (obj is not Id id)
        {
            throw new ArgumentException($"Must be {nameof(Id)}", nameof(obj));
        }

        return CompareTo(id);
    }

    public override readonly int GetHashCode() =>
        _value.GetHashCode();

    public override readonly string ToString() =>
        _value;

    public static bool operator <(Id left, Id right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(Id left, Id right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(Id left, Id right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(Id left, Id right)
    {
        return left.CompareTo(right) >= 0;
    }

    [return: NotNullIfNotNull(nameof(id))]
    public static implicit operator string?(Id? id)
    {
        return id?._value;
    }

    [return: NotNullIfNotNull(nameof(str))]
    public static implicit operator Id?(string? str)
    {
        if (str is null) { return null; }

        return Parse(str);
    }

    public static implicit operator Id(string str)
    {
        return Parse(str);
    }
}