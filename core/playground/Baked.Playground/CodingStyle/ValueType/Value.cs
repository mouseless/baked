using System.Diagnostics.CodeAnalysis;

namespace Baked.Playground.CodingStyle.ValueType;

public readonly record struct Value : IParsable<Value>, IEquatable<Value>
{
    public static Value Parse(string s,
        IFormatProvider? provider = default
    )
    {
        if (!TryParse(s, provider, out var result))
        {
            throw new FormatException($"'{s}' is not in an expected format");
        }

        return result;
    }

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        [MaybeNullWhen(false)] out Value result
    ) => TryParse(s, null, out result);

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out Value result
    )
    {
        result = new(s ?? string.Empty);

        return true;
    }

    readonly string _value;

    Value(string value)
    {
        _value = value;
    }

    public override string ToString() =>
        _value;
}