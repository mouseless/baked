using Baked.Architecture;
using Baked.Binding;
using Baked.Binding.Rest;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Domain.Model;
using Baked.RestApi;
using Baked.RestApi.Conventions;
using Baked.RestApi.Model;
using Humanizer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Nodes;
using System.Xml;

namespace Baked;

public static class RestBindingFeature
{
    public static IFeature<BindingFeature.Configurator> Rest(this BindingFeature.Configurator x) => x.Define((
        new TagDescriptions(),
        new RequestResponseExamples()
    ), (configurator, args) =>
    {
        var (tagDescriptions, examples) = args;

        configurator.Domain.ConfigureBuilder(builder =>
        {
            // domain attribute indices
            builder.Index.Type.Add<ApiController>();
            builder.Index.Type.Add<Bindable>();
            builder.Index.Method.Add<ApiAction>();
            builder.Index.Parameter.Add<ApiParameter>();
        });

        configurator.Domain.ConfigureConventions(conventions =>
        {
            // domain attribute mutations
            conventions.SetTypeAttribute(
                when: c =>
                  c.Type.Has<Service>() &&
                  c.Type.IsClass &&
                  !c.Type.IsAbstract &&
                  !c.Type.IsGenericType &&
                  c.Type.TryGetMembers(out var members) &&
                  members.Methods.Any(m => m.DefaultOverload.IsPublicInstanceWithNoSpecialName),
                attribute: c => new ApiController(),
                order: Order.At.Infra + 10
            );
            conventions.SetMethodAttribute(
                when: c =>
                    !c.Method.Has<External>() &&
                    !c.Method.Has<Initializer>() &&
                    c.Method.DefaultOverload.IsPublicInstanceWithNoSpecialName &&
                    c.Method.DefaultOverload.AllParametersAreBindable(),
                attribute: c => new ApiAction(),
                order: Order.At.Max
            );
            conventions.SetParameterAttribute(
                when: c => c.Parameter.IsBindable,
                attribute: c => new ApiParameter(),
                order: Order.At.Max
            );

            // init before any domain convention
            conventions.Add(new InitApiModelConvention(), order: Order.At.Global.AbsoluteMin);
            conventions.EditMethodAttribute<ApiAction>(
                attribute: (action, context) =>
                    action.Parameter[ApiParameter.TargetParameterName] =
                        new(ApiParameter.TargetParameterName, context.Type.CSharpFriendlyFullName, ParameterModelFrom.Services),
                order: Order.At.Global.AbsoluteMin
            );

            // rest api conventions
            conventions.Add(new AutoHttpMethodConvention([
                (Regexes.StartsWithGet, HttpMethod.Get),
                (Regexes.IsUpdateChangeOrSet, HttpMethod.Put),
                (Regexes.StartsWithUpdateChangeOrSet, HttpMethod.Patch),
                (Regexes.StartsWithDeleteRemoveOrClear, HttpMethod.Delete)
            ]), order: Order.At.Infra);
            conventions.Add(new GetAndDeleteAcceptsOnlyQueryConvention(), order: Order.At.Infra);
            conventions.Add(new RemoveFromRouteConvention(["Get"]), order: Order.At.Infra);
            conventions.Add(new RemoveFromRouteConvention(["Update", "Change", "Set"]), order: Order.At.Infra);
            conventions.Add(new RemoveFromRouteConvention(["Delete", "Remove", "Clear"]), order: Order.At.Infra);
            conventions.EditMethodAttribute<ApiAction>(
                when: (_, a) => a.HasBody,
                attribute: a => a.AdditionalAttributes.Add("Consumes(\"application/json\")"),
                order: Order.At.Infra + 10
            );
            conventions.EditMethodAttribute<ApiAction>(
                when: (_, a) => !a.ReturnIsVoid,
                attribute: a => a.AdditionalAttributes.Add("Produces(\"application/json\")"),
                order: Order.At.Infra + 10
            );
            conventions.Add(new UseDocumentationAsDescriptionConvention(tagDescriptions, examples), order: Order.At.Infra + 10);
            conventions.EditMethodAttribute<ApiAction>(
                (a, c) => a.AdditionalAttributes.Add($"{typeof(MappedMethod).FullName}(\"{c.Type.FullName}\", \"{c.Method.Name}\")"),
                order: Order.At.Infra
            );
            conventions.EditMethodAttribute<ApiAction>(
                when: c => c.Method.DefaultOverload.Parameters.Any(p => p.IsValidatable),
                attribute: a => a.AdditionalAttributes.Add($"ServiceFilter(typeof({typeof(RevalidateValidatableObjectsFilter).FullName}), Order = 1)"),
                order: Order.At.Infra
            );
        });

        configurator.Domain.ConfigureAttributeProperties(properties =>
        {
            properties.Set<ApiAction>(action =>
            [
                new("route", Value: $"{action.Method} /{action.GetRoute()}"),
                new("form", Value: action.UseForm),
                new("flat-request-body", Value: !action.UseRequestClassForBody)
            ]);
            properties.Set<ApiParameter>(parameter =>
            [
                new("required", parameter.FromRoute || parameter.HasRequiredAttributes),
                new("in", Value: parameter.FromBodyOrForm ? null : $"{parameter.From}".Kebaberize()),
                new("name", Value: parameter.Name != parameter.Id ? parameter.Name : null)
            ]);
        });

        configurator.Domain.ConfigureExportConfigurations(exports =>
        {
            exports.Build("RestApi", export =>
            {
                export
                    .Include<ApiController>()
                    .AddFilter(controller => controller.Actions.Any())
                ;
                export.Include<ApiAction>();
                export.Include<ApiParameter>();
                export.TypeGroupName(type =>
                    type.TryGetApiController(out var controller) ? controller.GroupName :
                    type.Name
                );
            });
        });

        configurator.RestApi.ConfigureApiModel(api =>
        {
            api.References.Add<BindingFeature.Configurator>();
            api.Usings.Add("Swashbuckle.AspNetCore.Annotations");

            configurator.Domain.UsingDomainModel(domain =>
            {
                foreach (var type in domain.Types.Having<ApiController>())
                {
                    if (!type.TryGetMetadata(out var metadata)) { continue; }

                    var controller = metadata.Get<ApiController>();
                    if (!controller.Action.Any()) { continue; }

                    api.Controllers.Add(controller);
                }
            });
        });

        configurator.Buildtime.ConfigureGeneratedFileCollection(files =>
        {
            files.AddAsJson(tagDescriptions);
            files.AddAsJson(examples);
        });

        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<RevalidateValidatableObjectsFilter>();
        });

        configurator.RestApi.ConfigureMvcNewtonsoftJsonOptions(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
            options.SerializerSettings.SerializationBinder = new PolymorphicSerializationBinder(options.SerializerSettings.SerializationBinder);
        });

        configurator.RestApi.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            swaggerGenOptions.EnableAnnotations();

            var schemaHelper = new SwaggerSchemaHelper();
            swaggerGenOptions.CustomSchemaIds(schemaHelper.GetSchemaId);

            swaggerGenOptions.OrderActionsBy(apiDescription =>
            {
                var methodOrder =
                    apiDescription.HttpMethod == "POST" ? 0 :
                    apiDescription.HttpMethod == "GET" ? 1 :
                    apiDescription.HttpMethod == "PUT" ? 2 :
                    apiDescription.HttpMethod == "PATCH" ? 3 :
                    4;

                return $"{apiDescription.ActionDescriptor.AttributeRouteInfo?.Template}_{methodOrder}";
            });
            swaggerGenOptions.SchemaFilter<RemoveNonPublicPropertiesSchemaFilter>();
            swaggerGenOptions.DocumentFilter<RemoveUnusedSchemasDocumentFilter>();

            configurator.Buildtime.UsingGeneratedContext(generatedContext =>
            {
                var tagDescriptions = generatedContext.ReadFileAsJson<TagDescriptions>() ?? [];
                swaggerGenOptions.DocumentFilter<ApplyTagDescriptionsDocumentFilter>(tagDescriptions);

                var examples = generatedContext.ReadFileAsJson<RequestResponseExamples>() ?? [];
                swaggerGenOptions.OperationFilter<XmlExamplesOperationFilter>(examples);
            });
        });
    });

    extension(DiagnosticCode)
    {
        public static DiagnosticCode RequiresInitializerAction => new(201, "requires-initializer-action");
        public static DiagnosticCode RequiresController => new(202, "requires-controller");
    }

    extension(TypeModel type)
    {
        public bool TryGetInitializerApiAction([NotNullWhen(true)] out ApiAction? action)
        {
            action = default;
            if (!type.TryGetMembers(out var members)) { return false; }

            var initializer = members.Methods.Having<Initializer>().SingleOrDefault();
            if (initializer is null) { return false; }
            if (!initializer.TryGet(out action)) { return false; }

            return true;
        }

        public ApiAction GetInitializerApiAction()
        {
            if (!type.TryGetInitializerApiAction(out var result))
            {
                throw DiagnosticCode.RequiresInitializerAction.Exception(
                    $"{type.Name} does not have an initializer that has an action model"
                );
            }

            return result;
        }

        public bool TryGetApiController([NotNullWhen(true)] out ApiController? controller)
        {
            controller = default;
            if (!type.TryGetMetadata(out var metadata)) { return false; }

            return metadata.TryGet(out controller);
        }

        public ApiController GetApiController()
        {
            if (!type.TryGetApiController(out var result))
            {
                throw DiagnosticCode.RequiresController.Exception(
                    $"{type.Name} does not have controller"
                );
            }

            return result;
        }
    }

    extension(MethodOverloadModel overload)
    {
        public bool IsPublicInstanceWithNoSpecialName =>
            overload.IsPublic && !overload.IsStatic && !overload.IsSpecialName;

        public bool AllParametersAreBindable() =>
            overload.Parameters.All(p => p.IsBindable);
    }

    extension(ParameterModel parameter)
    {
        public bool IsBindable =>
            parameter.ParameterType.TryGetMetadata(out var metadata) && metadata.Has<Bindable>();

        public bool IsValidatable =>
            parameter.ParameterType.SkipNullable().Apply(RevalidateValidatableObjectsFilter.IsValidatable);
    }

    extension(ApiDescription apiDescription)
    {
        public bool TryGetMappedMethod([NotNullWhen(true)] out MappedMethod? result)
        {
            result = apiDescription.CustomAttributes().OfType<MappedMethod>().SingleOrDefault();

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