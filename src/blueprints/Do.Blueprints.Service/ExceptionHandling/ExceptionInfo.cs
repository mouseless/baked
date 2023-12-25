namespace Do.ExceptionHandling;

public record ExceptionInfo(int Code, string Body, Dictionary<string, object?>? ExtraData = default);
