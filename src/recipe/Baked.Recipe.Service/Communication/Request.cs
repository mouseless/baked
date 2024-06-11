namespace Baked.Communication;

public record Request(
    string UrlOrPath,
    HttpMethod Method,
    Content? Content = default
)
{
    public Dictionary<string, string> Headers { get; init; } = [];

    public Request AddAuthorization(string authorization)
    {
        Headers["Authorization"] = authorization;

        return this;
    }
}