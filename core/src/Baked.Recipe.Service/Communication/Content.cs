using Newtonsoft.Json;

namespace Baked.Communication;

public sealed class Content
{
    public string Type { get; }
    public string Body { get; }
    public object? BodyAsObject { get; }
    public Dictionary<string, string>? BodyAsForm { get; }

    public Content(object? @object, JsonSerializerSettings? settings = default)
    {
        Type = "application/json";
        Body = JsonConvert.SerializeObject(@object, settings: settings);
        BodyAsObject = @object;
    }

    public Content(Dictionary<string, string> form)
    {
        Type = "application/x-www-form-urlencoded";
        Body = form.ToFormBody();
        BodyAsForm = form;
    }

    public override bool Equals(object? obj) =>
        obj is Content content &&
           Type == content.Type &&
           Body == content.Body;

    public override int GetHashCode() =>
        HashCode.Combine(Type, Body);

    public override string ToString() =>
        $"Content[Type = {Type}, Body = {Body}]";
}