namespace Baked;

public readonly record struct DiagnosticsCode(int Number)
{
    public static DiagnosticsCode Unknown => new(9999, "fatal");

    internal string? Key { get; }

    internal DiagnosticsCode(int number, string key)
        : this(number)
    {
        Key = key;
    }

    public DiagnosticsException Exception(string message) =>
        new(this, message);
}