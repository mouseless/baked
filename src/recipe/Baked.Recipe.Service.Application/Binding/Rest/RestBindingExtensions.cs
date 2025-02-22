using Baked.Binding;
using Baked.Binding.Rest;
using Baked.Domain.Model;
using Baked.RestApi.Model;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace Baked;

public static class RestBindingExtensions
{
    public static RestBindingFeature Rest(this BindingConfigurator _) =>
        new();

    public static bool IsPublicInstanceWithNoSpecialName(this MethodOverloadModel overload) =>
        overload.IsPublic && !overload.IsStatic && !overload.IsSpecialName;

    public static bool AllParametersAreApiInput(this MethodOverloadModel overload) =>
        overload.Parameters.All(IsApiInput);

    public static bool IsApiInput(this ParameterModel parameter) =>
        parameter.ParameterType.TryGetMetadata(out var metadata) && metadata.Has<ApiInputAttribute>();

    public static bool IsTarget(this ParameterModelAttribute parameter) =>
        parameter.Id == "target";

    public static bool TryGetMappedMethod(this ApiDescription apiDescription, [NotNullWhen(true)] out MappedMethodAttribute? result)
    {
        result = apiDescription.CustomAttributes().OfType<MappedMethodAttribute>().SingleOrDefault();

        return result is not null;
    }

    public static void SetJsonExample(this IDictionary<string, OpenApiMediaType> mediaTypes, XmlNode? documentation, string @for)
    {
        var example = documentation.GetExampleCode(@for);
        if (example is null) { return; }

        if (!mediaTypes.TryGetValue("application/json", out var mediaType))
        {
            mediaTypes["application/json"] = mediaType = new() { };
        }

        mediaType.Example = OpenApiAnyFactory.CreateFromJson(example);
    }

    public static void SetJsonExample(this IDictionary<string, OpenApiMediaType> mediaTypes, string? example)
    {
        if (example is null) { return; }

        if (!mediaTypes.TryGetValue("application/json", out var mediaType))
        {
            mediaTypes["application/json"] = mediaType = new() { };
        }

        mediaType.Example = OpenApiAnyFactory.CreateFromJson(example);
    }
}