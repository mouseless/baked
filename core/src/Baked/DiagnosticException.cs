namespace Baked;

public class DiagnosticException(DiagnosticCode code, string message)
    : Exception(message)
{
    public DiagnosticCode Code { get; } = code;
}