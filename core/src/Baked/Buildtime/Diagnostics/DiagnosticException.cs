namespace Baked.Buildtime.Diagnostics;

public class DiagnosticException(DiagnosticCode code, string message)
    : Exception(message)
{
    public DiagnosticCode Code { get; } = code;
}