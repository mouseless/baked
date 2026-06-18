namespace Baked.Domain.Configuration;

public readonly struct Order
{
    const int LEVEL_SPAN = 10000;
    const int ABSOLUTE_OFFSET = 10;

    static readonly Order _globalInstance = new(global: true);
    public static readonly Order At = new();

    readonly int _offset;
    readonly string? _base;
    readonly string? _level;
    readonly string? _extension;
    readonly bool _global;
    readonly int _lowerBound;
    readonly int _upperBound;

    public readonly Order Global => _globalInstance;
    public readonly Order AbsoluteMin => Clone(offset: _lowerBound);
    public readonly Order Min => Clone(offset: _lowerBound + ABSOLUTE_OFFSET);
    public readonly Order Zero => Clone(offset: 0);
    public readonly Order Max => Clone(offset: _upperBound - ABSOLUTE_OFFSET);
    public readonly Order AbsoluteMax => Clone(offset: _upperBound);

    public readonly string? Base => _base;
    public readonly string? Level => _level;
    public readonly string? Extension => _extension;
    public readonly bool IsGlobal => _global;

    public Order()
    {
        _lowerBound = -LEVEL_SPAN / 2;
        _upperBound = LEVEL_SPAN / 2 - 1;
    }

    Order(
        int offset = 0,
        string? @base = default,
        string? level = default,
        string? extension = default,
        bool global = false
    ) : this()
    {
        _offset = offset;
        _base = @base;
        _level = level;
        _extension = extension;
        _global = global;

        _lowerBound = _global ? int.MinValue : _lowerBound;
        _upperBound = _global ? int.MaxValue : _upperBound;
    }

    public Order Offset(int value) =>
        Clone(offset: _offset + value);

    public Order WithBase(string @base) =>
        Clone(@base: @base);

    public Order WithLevel(string level) =>
        Clone(level: level);

    public Order WithExtension(string extension) =>
        Clone(extension: extension);

    Order Clone(
        int? offset = default,
        string? @base = default,
        string? level = default,
        string? extension = default
    )
    {
        if (_global)
        {
            return new(offset: offset ?? _offset, global: true);
        }

        return new(
            offset: offset ?? _offset,
            @base: @base ?? _base,
            level: level ?? _level,
            extension: extension ?? _extension
        );
    }

    public int? Calculate(IReadOnlyDictionary<string, int> levels, string defaultLevel)
    {
        if (_global) { return _offset; }

        if (_base is null) { throw DiagnosticCode.InvalidOrder.Exception($"Order 'base' cannot be null"); }
        if (_level is null) { throw DiagnosticCode.InvalidOrder.Exception($"Order 'level' cannot be null"); }
        if (_extension is null) { throw DiagnosticCode.InvalidOrder.Exception($"Order 'extension' cannot be null"); }

        if (!levels.TryGetValue(defaultLevel, out var defaultLevelIndex))
        {
            throw DiagnosticCode.UndefinedLevel.Exception(
                $"Default level ({defaultLevel}) must be defined in levels"
            );
        }

        var level = $"{_base}.{_level}.{_extension}";
        if (!levels.TryGetValue(level, out var levelIndex)) { return null; }
        if (_offset < _lowerBound || _offset > _upperBound)
        {
            throw DiagnosticCode.OrderOutOfBounds.Exception(
                $"Order ({level}: {_offset}) must be between {_lowerBound} - {_upperBound}"
            );
        }

        return (levelIndex - defaultLevelIndex) * LEVEL_SPAN + _offset;
    }

    public override string ToString()
    {
        var baseCode = _base?[0] ?? '?';
        var levelCode = _level?[0] ?? '?';
        var extCode = _extension?[0] ?? '?';
        var offset = _offset >= 0 ? $"+{_offset:D4}" : $"{_offset:D4}";

        return $"{baseCode}{levelCode}{extCode}{offset}";
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