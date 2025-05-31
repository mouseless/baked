namespace Baked.Communication;

public class ClientException(string content, string? lKey, string[]? lParams, HttpRequestException inner)
    : Exception(inner.Message, inner)
{
    public string Content { get; } = content;
    public string? LKey { get; } = lKey;
    public string[]? LParams { get; } = lParams;
    public new HttpRequestException InnerException { get; } = inner;

    public ClientException(string content, HttpRequestException inner)
        : this(content, null, null, inner) { }
}