using System.Diagnostics.CodeAnalysis;

namespace Baked.Orm;

// Implement `IParsable<Id>` for model binding
public readonly struct Id(string _value) : IParsable<Id>
{
    public static Id Parse(object value) =>
        new($"{value}");

    public Id() : this(default!) { }

    public readonly string Value =>
        _value;

    public override readonly string ToString() =>
        Value;

    public override readonly bool Equals([NotNullWhen(true)] object? obj) =>
        obj is not null && obj is Id other && other.Value == Value;

    public override readonly int GetHashCode() =>
        Value.GetHashCode();

    static Id IParsable<Id>.Parse(string s, IFormatProvider? provider) =>
        new(s);

    static bool IParsable<Id>.TryParse(string? s, IFormatProvider? provider, out Id result)
    {
        result = new($"{s}");

        return true;
    }
}