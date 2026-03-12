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
    public class Configurator(LayerConfigurator _configurator)
    {
        public void ConfigureApiModel(Action<ApiModel> configuration) =>
            _configurator.Configure(configuration);

        public void ConfigureApplicationParts(Action<IApplicationPartCollection> configuration) =>
            _configurator.Configure(configuration);

        public void ConfigureMvcNewtonsoftJsonOptions(Action<MvcNewtonsoftJsonOptions> configuration) =>
            _configurator.Configure(configuration);

        public void ConfigureSwaggerGenOptions(Action<SwaggerGenOptions> configuration) =>
            _configurator.Configure(configuration);

        public void ConfigureSwaggerOptions(Action<SwaggerOptions> configuration) =>
            _configurator.Configure(configuration);

        public void ConfigureSwaggerUIOptions(Action<SwaggerUIOptions> configuration) =>
            _configurator.Configure(configuration);
    }

    extension(LayerConfigurator configurator)
    {
        public Configurator RestApi => new(configurator);
    }

    extension(IList<ILayer> layers)
    {
        public void AddRestApi() =>
            layers.Add(new RestApiLayer());
    }

    extension(IMvcBuilder mvcBuilder)
    {
        public IMvcBuilder AddApplicationParts(IApplicationPartCollection applicationParts)
        {
            foreach (var applicationPart in applicationParts)
            {
                mvcBuilder.AddApplicationPart(applicationPart.Assembly);
            }

            return mvcBuilder;
        }

        public IMvcBuilder AddNewtonsoftJson(MvcNewtonsoftJsonOptions options)
        {
            mvcBuilder.AddNewtonsoftJson();
            mvcBuilder.Services.AddOptions();
            mvcBuilder.Services.AddSingleton<IOptions<MvcNewtonsoftJsonOptions>>(sp =>
            {
                if (options.SerializerSettings.ContractResolver is IContractResolverWithServiceProvider contractResolver)
                {
                    contractResolver.ServiceProvider = sp;
                }

                return new OptionsWrapper<MvcNewtonsoftJsonOptions>(options);
            });

            return mvcBuilder;
        }
    }

    extension(ICollection<Assembly> assemblies)
    {
        public void Add<T>() =>
            assemblies.Add(typeof(T).Assembly);
    }

    extension(ActionModelAttribute action)
    {
        public void AddAttribute<T>() where T : Attribute =>
            action.AdditionalAttributes.Add(typeof(T).GetCSharpFriendlyFullName());

        public void MakeAsync()
        {
            if (action.ReturnIsAsync) { return; }

            action.ReturnIsAsync = true;
            action.ReturnType = action.ReturnIsVoid ? "Task" : $"Task<{action.ReturnType}>";
        }
    }

    extension(ParameterModelAttribute parameter)
    {
        public string GetRouteString()
        {
            var constraint = parameter switch
            {
                { Type: nameof(Guid) } => ":guid",
                _ when parameter.Type == typeof(Guid).FullName => ":guid",
                _ => string.Empty
            };

            return $"{{{parameter.Name}{constraint}}}";
        }
    }

    extension(List<string> parts)
    {
        public List<string> RemoveAll(string partToRemove)
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

        public List<string> Replace(string oldPart, string newPart)
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

    extension(IDomainModelConventionCollection conventions)
    {
        public void AddConfigureAction<T>(string name,
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
                order: RestApiLayer.MinConventionOrder + 10
            );
        }

        public void AddOverrideAction<T>(string name,
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
                order: RestApiLayer.MaxConventionOrder - 10
            );
        }
    }
}