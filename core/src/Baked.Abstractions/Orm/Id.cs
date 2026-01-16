namespace Baked.Orm;

public record struct Id : IParsable<Id>, IEquatable<Id>
{
    public static Id Parse(object value) =>
        new($"{value}");

    readonly string _value;

    Id(string value)
    {
        _value = value;
    }

    public override readonly string ToString() =>
        _value;

    // Implement `IParsable<Id>` for model binding
    static Id IParsable<Id>.Parse(string s, IFormatProvider? provider) =>
        Parse(s);

    static bool IParsable<Id>.TryParse(string? s, IFormatProvider? provider, out Id result)
    {
        result = Parse($"{s}");

        return true;
    }
}