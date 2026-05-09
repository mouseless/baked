namespace Baked.Buildtime.Diagnostics;

public record DiagnosticsResult(
    IReadOnlyCollection<Exception> Exceptions,
    IReadOnlyCollection<DiagnosticMessage> Messages
);