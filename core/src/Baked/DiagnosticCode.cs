namespace Baked;

public readonly record struct DiagnosticCode(int Number)
{
    public static DiagnosticCode Unknown => new(9999, "unknown");

    public string? Key { get; }

    internal DiagnosticCode(int number, string key)
        : this(number)
    {
        Key = key;
    }

    public DiagnosticException Exception(string message) =>
        new(this, message);
}