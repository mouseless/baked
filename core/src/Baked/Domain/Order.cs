namespace Baked.Domain;

public struct Order
{
    public const int OrderSpan = 1000;

    public static Order FromLevel(string level) =>
        new(level: level);

    string? _level;
    int _offset = 0;
    int _base = 0;

    Order(
        string? level = default,
        int offset = 0
    )
    {
        _level = level;
        _offset = offset;
    }

#pragma warning disable IDE0251 // Make member 'readonly'
    public int Offset => _offset;
    public string? Level => _level;
    public Order AbsoluteMin => new(level: _level, offset: -OrderSpan);
    public Order Min => new(level: _level, offset: -OrderSpan + 10);
    public Order Max => new(level: _level, offset: OrderSpan - 10);
    public Order AbsoluteMax => new(level: _level, offset: OrderSpan);
#pragma warning restore IDE0251 // Make member 'readonly'
    internal Order SetBase(int @base)
    {
        _base = @base;

        return this;
    }

    public static implicit operator int(Order order) =>
        order._base + order._offset;

    public static implicit operator Order(int value) =>
        new(offset: value);
}