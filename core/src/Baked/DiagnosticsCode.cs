namespace Baked;

public readonly record struct DiagnosticsCode(int Number)
{
    internal string? Key { get; }

    internal DiagnosticsCode(int number, string key)
        : this(number)
    {
        Key = key;
    }

    public static DiagnosticsCode Unknown => new(9999, "fatal");
}