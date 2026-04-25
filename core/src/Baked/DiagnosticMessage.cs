namespace Baked;

public class DiagnosticMessage(string _message, string _level,
    DiagnosticCode? _code = default
)
{
    public string Level => _level;
    public string Message => _message;
    public DiagnosticCode? Code => _code;

    string ErrorCode =>
        _code is null ? string.Empty :
        _code.Value.Key is null ? $" C{_code.Value.Number:D4}" :
        $" [link=https://baked.mouseless.codes/errors#{_code.Value.Key}]B{_code.Value.Number:D4}[/]";

    string Color =>
        _level switch
        {
            "error" => "maroon",
            "warning" => "darkorange3",
            "info" => "cyan",
            _ => "default"
        };

    public override string ToString() =>
        $"[bold {Color}]{_level}{ErrorCode}[/]: {_message}";
}