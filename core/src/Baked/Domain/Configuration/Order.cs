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

    public Order WithBase(string @base) => Clone(
        @base: @base,
        global: false
    );

    public Order WithLevel(string level) => Clone(
        level: level,
        global: false
    );

    public Order WithExtension(string extension) => Clone(
        extension: extension,
        global: false
    );

    Order Clone(
        int? offset = default,
        string? @base = default,
        string? level = default,
        string? extension = default,
        bool? global = default
    ) => new(
            offset: offset ?? _offset,
            @base: @base ?? _base,
            level: level ?? _level,
            extension: extension ?? _extension,
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

        if (_base is null) { throw new("no base"); }
        if (_level is null) { throw new("no level"); }
        if (_extension is null) { throw new("no extension"); }

        var level = $"{_base}.{_level}.{_extension}";
        if (_offset < _lowerBound || _offset > _upperBound)
        {
            throw DiagnosticCode.OrderOutOfBounds.Exception(
                $"Order ({level}: {_offset}) must be between {_lowerBound} - {_upperBound}"
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