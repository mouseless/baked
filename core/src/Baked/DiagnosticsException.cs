namespace Baked;

public class DiagnosticsException(DiagnosticsCode code, string message)
    : Exception(message)
{
    public DiagnosticsCode Code { get; } = code;
}