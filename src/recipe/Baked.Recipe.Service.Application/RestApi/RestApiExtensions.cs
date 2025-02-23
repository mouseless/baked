using Baked.Architecture;
using Baked.Domain;
using Baked.RestApi;
using Baked.RestApi.Conventions;
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

    public static void Add<T>(this ICollection<Assembly> assemblies) =>
        assemblies.Add(typeof(T).Assembly);

    public static string GetControllerId(this Type type) =>
        type.GetCSharpFriendlyFullName();

    public static ControllerModelAttribute GetController<T>(this ApiModel api) =>
        api.Controller[typeof(T).GetControllerId()];

    public static void AddAttribute<T>(this ActionModelAttribute action) where T : Attribute =>
        action.AdditionalAttributes.Add(typeof(T).GetCSharpFriendlyFullName());

    public static string GetRouteString(this ParameterModelAttribute parameter)
    {
        var constraint = parameter switch
        {
            { Type: nameof(Guid) } => ":guid",
            // _ when parameter.TypeModel.Is<Guid>() => ":guid",
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

    public static void AddConfigureAction<T>(this IDomainModelConventionCollection conventions, string name,
        HttpMethod? method = default,
        List<string>? routeParts = default,
        bool? useForm = default,
        bool? useRequestClassForBody = default,
        Action<Dictionary<string, ParameterModelAttribute>>? parameter = default
    )
    {
        conventions.Add(
            new ConfigureActionConvention<T>(name, action =>
            {
                if (method is not null) { action.Method = method; }
                if (routeParts is not null) { action.RouteParts = routeParts; }
                if (useForm is not null) { action.UseForm = useForm.Value; }
                if (useRequestClassForBody is not null) { action.UseRequestClassForBody = useRequestClassForBody.Value; }
                if (parameter is not null) { parameter(action.Parameter); }
            }),
            order: int.MinValue + 10
        );
    }

    public static void OverrideAction<T>(this ApiModel api, string name,
        HttpMethod? method = default,
        List<string>? routeParts = default,
        bool? useForm = default,
        bool? useRequestClassForBody = default,
        Action<Dictionary<string, ParameterModelAttribute>>? parameter = default
    )
    {
        var controller = api.GetController<T>();
        var action = controller.Action[name];

        if (method is not null) { action.Method = method; }
        if (routeParts is not null) { action.RouteParts = routeParts; }
        if (useForm is not null) { action.UseForm = useForm.Value; }
        if (useRequestClassForBody is not null) { action.UseRequestClassForBody = useRequestClassForBody.Value; }
        if (parameter is not null) { parameter(action.Parameter); }
    }
}