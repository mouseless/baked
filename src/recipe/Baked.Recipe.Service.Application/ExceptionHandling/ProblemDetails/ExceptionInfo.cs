namespace Baked.ExceptionHandling.ProblemDetails;

public record ExceptionInfo(
    Exception Exception,
    int Code,
    string Body,
    Dictionary<string, object?>? ExtraData = default
);