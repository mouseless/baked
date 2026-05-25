namespace Baked.Domain;

public partial record struct Order : IComparable<Order>, IEquatable<Order>
{
    public readonly int CompareTo(Order other) =>
        _value.CompareTo(other._value);

    public readonly bool Equals(Order other) =>
        _value.Equals(other._value);

    public override readonly int GetHashCode() =>
        _value.GetHashCode();

    public static implicit operator int(Order order) =>
        order._value;

    public static implicit operator Order(int value) =>
        new(offset: value);

    public static bool operator <=(Order a, Order b)
    {
        return a._value <= b._value;
    }

    public static bool operator >=(Order a, Order b)
    {
        return a._value >= b._value;
    }

    public static bool operator <(Order a, Order b)
    {
        return a._value < b._value;
    }

    public static bool operator >(Order a, Order b)
    {
        return a._value > b._value;
    }
}
