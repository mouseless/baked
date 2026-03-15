using Baked.Binding;
using Baked.Binding.Rest;
using Baked.Business;
using Baked.Domain.Model;
using Baked.RestApi.Model;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Nodes;
using System.Xml;

namespace Baked;

public static class RestBindingExtensions
{
    extension(BindingConfigurator _)
    {
        public RestBindingFeature Rest() =>
            new();
    }

    extension(TypeModel type)
    {
        public bool TryGetInitializerActionModel([NotNullWhen(true)] out ActionModelAttribute? action)
        {
            action = default;
            if (!type.TryGetMembers(out var members)) { return false; }

            var initializer = members.Methods.Having<InitializerAttribute>().SingleOrDefault();
            if (initializer is null) { return false; }
            if (!initializer.TryGet(out action)) { return false; }

            return true;
        }

        public ActionModelAttribute GetInitializerActionModel()
        {
            if (!type.TryGetInitializerActionModel(out var result)) { throw new($"{type.Name} does not have action model"); }

            return result;
        }

        public bool TryGetControllerModel([NotNullWhen(true)] out ControllerModelAttribute? controller)
        {
            controller = default;
            if (!type.TryGetMetadata(out var metadata)) { return false; }

            return metadata.TryGet(out controller);
        }

        public ControllerModelAttribute GetControllerModel()
        {
            if (!type.TryGetControllerModel(out var result)) { throw new($"{type.Name} does not have controller"); }

            return result;
        }
    }

    extension(MethodOverloadModel overload)
    {
        public bool IsPublicInstanceWithNoSpecialName =>
            overload.IsPublic && !overload.IsStatic && !overload.IsSpecialName;

        public bool AllParametersAreApiInput() =>
            overload.Parameters.All(p => p.IsApiInput);
    }

    extension(ParameterModel parameter)
    {
        public bool IsApiInput =>
            parameter.ParameterType.TryGetMetadata(out var metadata) && metadata.Has<ApiInputAttribute>();
    }

    extension(ApiDescription apiDescription)
    {
        public bool TryGetMappedMethod([NotNullWhen(true)] out MappedMethodAttribute? result)
        {
            result = apiDescription.CustomAttributes().OfType<MappedMethodAttribute>().SingleOrDefault();

            return result is not null;
        }
    }

    extension(IDictionary<string, OpenApiMediaType> mediaTypes)
    {
        public void SetJsonExample(XmlNode? documentation, string @for)
        {
            var example = documentation.GetExampleCode(@for);
            if (example is null) { return; }

            if (!mediaTypes.TryGetValue("application/json", out var mediaType))
            {
                mediaTypes["application/json"] = mediaType = new() { };
            }

            mediaType.Example = JsonNode.Parse(example);
        }

        public void SetJsonExample(string? example)
        {
            if (example is null) { return; }

            if (!mediaTypes.TryGetValue("application/json", out var mediaType))
            {
                mediaTypes["application/json"] = mediaType = new() { };
            }

            mediaType.Example = JsonNode.Parse(example);
        }
    }
}