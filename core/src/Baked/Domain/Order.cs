namespace Baked.Domain;

public struct Order : IComparable<Order>, IEquatable<Order>
{
    int _value;

    public readonly int CompareTo(Order other) =>
        _value.CompareTo(other._value);

    public readonly bool Equals(Order other) =>
        _value.Equals(other._value);

    public override readonly bool Equals(object? obj) =>
        obj is Order order && Equals(order);

    public override readonly int GetHashCode() =>
        _value.GetHashCode();

    public static implicit operator int(Order order) =>
        order._value;

    public static implicit operator Order(int value)
    {
        var result = new Order
        {
            _value = value
        };

        return result;
    }

    public static bool operator ==(Order a, Order b)
    {
        return a._value == b._value;
    }

    public static bool operator !=(Order a, Order b)
    {
        return a._value != b._value;
    }

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