namespace Baked.Domain;

public struct Order
{
    public static Order Global = new(level: "Global");

    public const int LevelSpan = 10000;

    public static Order FromLevel(string level) =>
        new(level: level);

    int _base = default;
    int _offset = default;
    string? _level;

    Order(
        int @base = 0,
        int offset = 0,
        string? level = default
    )
    {
        _base = @base;
        _offset = offset;
        _level = level;
    }

    public readonly bool IsGlobal => _level == "Global";
    public readonly string? Level => _level;
    public readonly Order AbsoluteMin => new(@base: _base, offset: -LevelSpan / 2, level: _level);
    public readonly Order Min => new(@base: _base, offset: -LevelSpan / 2 + 10, level: _level);
    public readonly Order Max => new(@base: _base, offset: LevelSpan / 2 - 11, level: _level);
    public readonly Order AbsoluteMax => new(@base: _base, offset: LevelSpan / 2 - 1, level: _level);

    internal readonly Order WithBase(int @base) =>
        new(@base: @base, offset: _offset, level: _level);

    public static implicit operator int(Order order)
    {
        if (!order.IsGlobal && order._offset > LevelSpan / 2 - 1)
        {
            throw DiagnosticCode.OrderOutOfBounds.Exception("Order cannot exceed allowed absolute max value");
        }

        if (!order.IsGlobal && order._offset < -LevelSpan / 2)
        {
            throw DiagnosticCode.OrderOutOfBounds.Exception("Order cannot be lower than allowed absolute min value");
        }

        return order._base * LevelSpan + order._offset;
    }

    public static implicit operator Order(int value) =>
        new(offset: value);

    public static Order operator +(Order left, int offset)
    {
        return new(@base: left._base, offset: left._offset + offset, level: left._level);
    }

    public static Order operator -(Order left, int offset)
    {
        return new(@base: left._base, offset: left._offset - offset, level: left._level);
    }
}