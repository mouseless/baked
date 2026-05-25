namespace Baked.Domain;

public partial record struct Order
{
    public const int OrderSpan = 1000;

    public static Order FromLevel(string level) =>
        new(level: level);

    string? _level;
    int _offset = 0;
    int _value;

    Order(
        string? level = default,
        int offset = 0
    )
    {
        _level = level;
        _offset = offset;
    }

    public readonly int Offset => _offset;
    public readonly string? Level => _level;
    public readonly int Value => _value;
    public Order AbsoluteMin => this with { _offset = -OrderSpan };
    public Order Min => this with { _offset = -OrderSpan + 10 };
    public Order Max => this with { _offset = OrderSpan - 10 };
    public Order AbsoluteMax => this with { _offset = OrderSpan };

    internal void SetValue(int @base)
    {
        _value = @base + _offset;
    }
}