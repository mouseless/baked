using Newtonsoft.Json;
using System.Net;

namespace Baked.Communication;

public record Response(
    HttpStatusCode StatusCode,
    string Content
)
{
    public static Response SuccessResponse(string Content) =>
        new(HttpStatusCode.OK, Content);

    public bool HasContent => !string.IsNullOrWhiteSpace(Content);
    public bool IsSuccessStatusCode => StatusCode == HttpStatusCode.OK;

    public dynamic? GetContentAsObject(
        JsonSerializerSettings? settings = default
    ) => JsonConvert.DeserializeObject<dynamic>(Content, settings ?? new());
}