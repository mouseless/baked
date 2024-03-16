using Newtonsoft.Json;
using Shouldly;
using System.Diagnostics.CodeAnalysis;

namespace Do;

public static class JsonExtensions
{
    public static void ShouldDeeplyBe(this object? payload, object? json) => payload.ToJsonString().ShouldBe(json.ToJsonString());

    [return: NotNullIfNotNull("payload")]
    public static string? ToJsonString(this object? payload) => payload is null ? null : JsonConvert.SerializeObject(payload);
    [return: NotNullIfNotNull("payload")]
    public static object? ToJsonObject(this object? payload) => JsonConvert.DeserializeObject(payload.ToJsonString() ?? string.Empty);
}
