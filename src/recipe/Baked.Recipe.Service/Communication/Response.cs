using Newtonsoft.Json;

namespace Baked.Communication;

public record Response(
    string Content
)
{
    public bool HasContent => !string.IsNullOrWhiteSpace(Content);

    public dynamic? GetContentAsObject(
        JsonSerializerSettings? settings = default
    ) => JsonConvert.DeserializeObject<dynamic>(Content, settings ?? new());
}