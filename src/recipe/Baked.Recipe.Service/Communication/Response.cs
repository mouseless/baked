using Newtonsoft.Json;

namespace Baked.Communication;

public record Response(
    StatusCode StatusCode,
    string Content
)
{
    public bool HasContent => !string.IsNullOrWhiteSpace(Content);
    public bool IsSuccess => StatusCode == StatusCode.Success;
    public bool IsError => StatusCode is StatusCode.Handled or StatusCode.Unhandled;

    public dynamic? GetContentAsObject(
        JsonSerializerSettings? settings = default
    ) => JsonConvert.DeserializeObject<dynamic>(Content, settings ?? new());
}