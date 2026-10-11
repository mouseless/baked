namespace Baked.Buildtime.Diagnostics;

public class DiagnosticMessage(string message, string level,
    DiagnosticCode? code = default,
    string? group = default
)
{
    public string Level { get; } = level;
    public string Message { get; } = message;
    public DiagnosticCode? Code { get; } = code;
    public string Group { get; } = group ?? "_";

    string ErrorCode =>
        Code is null ? string.Empty :
        Code.Value.Key is null ? $" C{Code.Value.Number:D4}" :
        $" [link=https://baked.mouseless.codes/errors#{Code.Value.Key}]B{Code.Value.Number:D4}[/]";

    string Color =>
        Level switch
        {
            "error" => "maroon",
            "warning" => "darkorange3",
            "info" => "cyan",
            _ => "default"
        };

    public override string ToString() =>
        $"[bold {Color}]{Level}{ErrorCode}[/]: {Message}";
}