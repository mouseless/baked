using Baked.Architecture;
using Baked.RestApi;
using Baked.RestApi.Configuration;
using Baked.RestApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace Baked;

public static class RestApiExtensions
{
    public static void AddRestApi(this IList<ILayer> layers) =>
        layers.Add(new RestApiLayer());

    public static void ConfigureApiModel(this LayerConfigurator configurator, Action<ApiModel> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigureApiModelConventions(this LayerConfigurator configurator, Action<IApiModelConventionCollection> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigureApplicationParts(this LayerConfigurator configurator, Action<IApplicationPartCollection> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigureMvcNewtonsoftJsonOptions(this LayerConfigurator configurator, Action<MvcNewtonsoftJsonOptions> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigureSwaggerGenOptions(this LayerConfigurator configurator, Action<SwaggerGenOptions> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigureSwaggerOptions(this LayerConfigurator configurator, Action<SwaggerOptions> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigureSwaggerUIOptions(this LayerConfigurator configurator, Action<SwaggerUIOptions> configuration) =>
        configurator.Configure(configuration);

    public static IMvcBuilder AddApplicationParts(this IMvcBuilder mvcBuilder, IApplicationPartCollection applicationParts)
    {
        foreach (var applicationPart in applicationParts)
        {
            mvcBuilder.AddApplicationPart(applicationPart.Assembly);
        }

        return mvcBuilder;
    }

    public static IMvcBuilder AddNewtonsoftJson(this IMvcBuilder mvcBuilder, MvcNewtonsoftJsonOptions options)
    {
        mvcBuilder.AddNewtonsoftJson();
        mvcBuilder.Services.AddOptions();
        mvcBuilder.Services.AddSingleton<IOptions<MvcNewtonsoftJsonOptions>>(sp => new OptionsWrapper<MvcNewtonsoftJsonOptions>(options));

        return mvcBuilder;
    }

    public static void Add(this IApiModelConventionCollection collection, IApiModelConvention convention,
        int order = 0
    ) => collection.Add((convention, order));

    public static void Add<T>(this ICollection<Assembly> assemblies) =>
        assemblies.Add(typeof(T).Assembly);

    public static string GetControllerId(this Type type) =>
        type.GetCSharpFriendlyFullName();

    public static ControllerModel GetController<T>(this ApiModel api) =>
        api.Controller[typeof(T).GetControllerId()];

    public static void AddAttribute<T>(this ActionModel action) where T : Attribute =>
        action.AdditionalAttributes.Add(typeof(T).GetCSharpFriendlyFullName());

    public static string GetRouteString(this ParameterModel parameter)
    {
        var constraint = parameter switch
        {
            { Type: nameof(Guid) } => ":guid",
            _ when parameter.TypeModel.Is<Guid>() => ":guid",
            _ => string.Empty
        };

        return $"{{{parameter.Name}{constraint}}}";
    }

    public static List<string> RemoveAll(this List<string> parts, string partToRemove)
    {
        for (var i = 0; i < parts.Count; i++)
        {
            if (parts[i] == partToRemove)
            {
                parts.RemoveAt(i);
                i--;
            }
        }

        return parts;
    }

    public static List<string> Replace(this List<string> parts, string oldPart, string newPart)
    {
        for (var i = 0; i < parts.Count; i++)
        {
            if (parts[i] == oldPart)
            {
                parts[i] = newPart;
            }
        }

        return parts;
    }
}