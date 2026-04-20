namespace Baked.CodeGeneration;

public record DiagnosticsResult(
    IReadOnlyCollection<Exception> Errors,
    IReadOnlyCollection<string> Messages
);