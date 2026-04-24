namespace Baked;

public class DiagnosticMessage(string _message, string _level,
    DiagnosticCode? _code = default
)
{
    const string Esc = "\x1b";
    const string Cyan = $"{Esc}[1m{Esc}[36m";
    const string Red = $"{Esc}[1m{Esc}[31m";
    const string Orange = $"{Esc}[1m{Esc}[33m";
    const string Reset = $"{Esc}[0m";

    public string Level => _level;
    public string Message => _message;
    public DiagnosticCode? Code => _code;

    string ErrorCode =>
        _code is null ? string.Empty :
        _code.Value.Key is null ? $" C{_code.Value.Number:D4}" :
        $" {Esc}]8;;https://baked.mouseless.codes/errors#{_code.Value.Key}{Esc}\\B{_code.Value.Number:D4}{Esc}]8;;{Esc}\\";

    string Color =>
        _level switch
        {
            "error" => Red,
            "warning" => Orange,
            "info" => Cyan,
            _ => Reset
        };

    public override string ToString() =>
        $"{Color}{_level}{ErrorCode}{Reset}: {_message}";
}