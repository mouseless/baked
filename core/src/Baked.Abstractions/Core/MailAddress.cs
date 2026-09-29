using System.Diagnostics.CodeAnalysis;

using SystemMail = System.Net.Mail;

namespace Baked.Core;

public readonly struct MailAddress(SystemMail.MailAddress value)
    : IParsable<MailAddress>, IEquatable<MailAddress>
{
    public static MailAddress Parse(string s,
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
        [MaybeNullWhen(false)] out MailAddress result
    ) => TryParse(s, null, out result);

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out MailAddress result
    )
    {
        result = default;
        if (string.IsNullOrWhiteSpace(s)) { return false; }

        if (!SystemMail.MailAddress.TryCreate(s, out var ma))
        {
            return false;
        }

        if (!ma.Host.Contains('.') || ma.Host.Contains('[')) { return false; }

        result = new(ma);

        return true;
    }

    readonly SystemMail.MailAddress _value = new(value.Address.ToLowerInvariant(), value.DisplayName);

    public override readonly string ToString() =>
        this == default(MailAddress) ? string.Empty : _value.ToString().ToLowerInvariant();

    public bool Equals(MailAddress other) =>
        string.Equals(_value?.Address, other._value?.Address, StringComparison.OrdinalIgnoreCase);

    public override bool Equals(object? other)
    {
        if (other is not MailAddress ma) { return false; }

        return Equals(ma);
    }

    public override int GetHashCode() =>
        HashCode.Combine(_value);

    public static implicit operator MailAddress(SystemMail.MailAddress v)
    {
        ArgumentNullException.ThrowIfNull(v);

        return new MailAddress(v);
    }

    public static implicit operator SystemMail.MailAddress(MailAddress ma)
    {
        return ma._value;
    }

    public static bool operator ==(MailAddress? left, string? right)
    {
        return string.Equals(left?.ToString(), right?.ToLowerInvariant(), StringComparison.OrdinalIgnoreCase);
    }

    public static bool operator !=(MailAddress? left, string? right)
    {
        return !(left == right);
    }

    public static bool operator ==(MailAddress? left, MailAddress? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(MailAddress? left, MailAddress? right)
    {
        return !(left == right);
    }
}