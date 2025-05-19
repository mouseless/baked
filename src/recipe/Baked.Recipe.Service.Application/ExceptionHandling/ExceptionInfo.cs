namespace Baked.ExceptionHandling;

public record ExceptionInfo(
    Exception Exception,
    int Code,
    string Body,
    string? LKey = default,
    string[]? LParams = default,
    Dictionary<string, object?>? ExtraData = default
);