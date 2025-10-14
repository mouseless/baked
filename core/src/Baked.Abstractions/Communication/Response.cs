using Newtonsoft.Json;
using System.Net;

namespace Baked.Communication;

public record Response(
    HttpStatusCode StatusCode,
    string Content
)
{
    public bool HasContent => !string.IsNullOrWhiteSpace(Content);
    public bool IsSuccess => (int)StatusCode < 400;

    public dynamic? GetContentAsObject(
        JsonSerializerSettings? settings = default
    ) => JsonConvert.DeserializeObject<dynamic>(Content, settings ?? new());
}