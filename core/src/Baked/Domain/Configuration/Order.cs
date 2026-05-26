namespace Baked.Domain.Configuration;

public readonly struct Order
{
    const int LEVEL_SPAN = 10000;
    const int ABSOLUTE_OFFSET = 10;

    static readonly Order _globalInstance = new(global: true);
    public static readonly Order At = new();

    readonly int _offset;
    readonly string? _level;
    readonly bool _global;
    readonly int _lowerBound;
    readonly int _upperBound;

    public readonly Order Global => _globalInstance;
    public readonly Order AbsoluteMin => Clone(offset: _lowerBound);
    public readonly Order Min => Clone(offset: _lowerBound + ABSOLUTE_OFFSET);
    public readonly Order Zero => Clone(offset: 0);
    public readonly Order Max => Clone(offset: _upperBound - ABSOLUTE_OFFSET);
    public readonly Order AbsoluteMax => Clone(offset: _upperBound);

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
        Clone(offset: _offset + value);

    public Order Level(string level) =>
        Clone(level: level, global: false);

    Order Clone(
        int? offset = default,
        string? level = default,
        bool? global = default
    ) => new(
            offset: offset ?? _offset,
            level: level ?? _level,
            global: global ?? _global
        );

    public int Calculate(IReadOnlyDictionary<string, int> levels, string defaultLevel)
    {
        if (_global) { return _offset; }

        if (!levels.TryGetValue(defaultLevel, out var defaultLevelIndex))
        {
            throw DiagnosticCode.UndefinedLevel.Exception(
                $"Default level ({defaultLevel}) must be defined in levels"
            );
        }

        if (_offset < _lowerBound || _offset > _upperBound)
        {
            throw DiagnosticCode.OrderOutOfBounds.Exception(
                $"Order ({_level ?? defaultLevel}: {_offset}) must be between {_lowerBound} - {_upperBound}"
            );
        }

        var levelIndex = defaultLevelIndex;
        if (_level is not null && !levels.TryGetValue(_level, out levelIndex))
        {
            Diagnostics.Current.ReportWarning(DiagnosticCode.UndefinedLevel,
                $"Given level '{_level}' was not found in configured levels, defaulting to '{defaultLevel}'"
            );
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