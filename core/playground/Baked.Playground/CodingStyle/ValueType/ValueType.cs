using System.Diagnostics.CodeAnalysis;

namespace Baked.CodingStyle.ValueType;

public readonly record struct ValueType : IParsable<ValueType>, IEquatable<ValueType>
{
    public static ValueType Parse(string s, IFormatProvider? provider) =>
        new(s);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ValueType result)
    {
        result = Parse(s ?? string.Empty, provider);

        return true;
    }

    readonly string _value;

    ValueType(string value)
    {
        _value = value;
    }

    public override string ToString() =>
        _value;
}