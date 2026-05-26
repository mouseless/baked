namespace Baked.Domain;

public struct Order
{
    public static Order Global => new Order(level: "Global");
    public static Order Default => new Order();

    public static Order FromLevel(string level) =>
        new(level: level);

    public const int LevelSpan = 10000;

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
    public readonly Order AbsoluteMin => new(@base: _base, offset: LowerBound, level: _level);
    public readonly Order Min => new(@base: _base, offset: LowerBound + 10, level: _level);
    public readonly Order Max => new(@base: _base, offset: UpperBound - 10, level: _level);
    public readonly Order AbsoluteMax => new(@base: _base, offset: UpperBound, level: _level);

    readonly int BaseValue => IsGlobal ? 0 : _base * LevelSpan;
    readonly int LowerBound => IsGlobal ? int.MinValue : -LevelSpan / 2;
    readonly int UpperBound => IsGlobal ? int.MaxValue : LevelSpan / 2 - 1;

    internal readonly Order WithBase(int @base) =>
        new(@base: @base, offset: _offset, level: _level);

    internal readonly int CalculateValue()
    {
        if (_offset > UpperBound)
        {
            throw DiagnosticCode.OrderOutOfBounds.Exception("Order cannot exceed allowed absolute max value");
        }

        if (_offset < LowerBound)
        {
            throw DiagnosticCode.OrderOutOfBounds.Exception("Order cannot be lower than allowed absolute min value");
        }

        return BaseValue + _offset;
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