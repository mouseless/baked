namespace Baked.Domain.Configuration;

public readonly struct Order
{
    const int LEVEL_SPAN = 10000;
    const int ABSOLUTE_OFFSET = 10;

    static readonly Order _globalInstance = new(global: true);
    public static Order Create = new();

    readonly int _offset;
    readonly string? _level;
    readonly bool _global;
    readonly int _lowerBound;
    readonly int _upperBound;

    public Order Global => _globalInstance;
    public readonly Order AbsoluteMin => new(offset: _lowerBound, level: _level, global: _global);
    public readonly Order Min => new(offset: _lowerBound + ABSOLUTE_OFFSET, level: _level, global: _global);
    public readonly Order Max => new(offset: _upperBound - ABSOLUTE_OFFSET, level: _level, global: _global);
    public readonly Order AbsoluteMax => new(offset: _upperBound, level: _level, global: _global);

    public Order()
    {
        _lowerBound = -LEVEL_SPAN / 2;
        _upperBound = LEVEL_SPAN / 2 - 1;
    }

    Order(
        int offset = 0,
        string? level = default,
        bool global = false
    ) : this()
    {
        _offset = offset;
        _level = level;
        _global = global;

        _lowerBound = _global ? int.MinValue : _lowerBound;
        _upperBound = _global ? int.MaxValue : _upperBound;
    }

    public Order Offset(int value) =>
        new(offset: _offset + value, level: _level, global: _global);

    public Order Level(string level) =>
        new(level: level);

    public int Calculate(IReadOnlyDictionary<string, int> levels, string defaultLevel)
    {
        if (_offset < _lowerBound || _offset > _upperBound)
        {
            throw DiagnosticCode.OrderOutOfBounds.Exception($"Order offset ({_offset}) must be between {_lowerBound} - {_upperBound}");
        }

        if (!levels.TryGetValue(defaultLevel, out var defaultLevelIndex))
        {
            throw DiagnosticCode.UndefinedLevel.Exception($"Default level ({defaultLevel}) must be defined in levels");
        }

        if (_global) { return _offset; }

        if (_level is null || !levels.TryGetValue(_level, out var levelIndex))
        {
            Diagnostics.Current.ReportWarning(DiagnosticCode.UndefinedLevel,
                _level is null
                    ? $"Level not specified, defaulting to '{defaultLevel}'"
                    : $"Given level '{_level}' was not found in configured levels, defaulting to '{defaultLevel}'"
                );

            levelIndex = defaultLevelIndex;
        }

        return (levelIndex - defaultLevelIndex) * LEVEL_SPAN + _offset;
    }

    public static implicit operator Order(int value) =>
        new(offset: value);

    public static Order operator +(Order left, int offset)
    {
        return left.Offset(offset);
    }

    public static Order operator -(Order left, int offset)
    {
        return left.Offset(-offset);
    }
}