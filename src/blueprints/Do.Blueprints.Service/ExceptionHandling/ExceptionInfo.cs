namespace Do.ExceptionHandling;

public record ExceptionInfo(
    Exception Exception,
    int Code,
    string Body,
    Dictionary<string, object?>? ExtraData = default
);