namespace Baked;

public record DiagnosticsResult(
    IReadOnlyCollection<Exception> Errors,
    IReadOnlyCollection<string> Messages
);