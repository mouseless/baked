using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi;
using Baked.RestApi.Conventions;
using Baked.RestApi.Model;
using Humanizer;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Baked.Binding.Rest;

public class RestBindingFeature : IFeature<BindingConfigurator>
{
    readonly TagDescriptions _tagDescriptions = new();
    readonly RequestResponseExamples _examples = [];

    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureBuilder(builder =>
        {
            // domain attribute indices
            builder.Index.Type.Add<ApiController>();
            builder.Index.Type.Add<ApiInputAttribute>();
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
                    !c.Method.Has<ExternalAttribute>() &&
                    !c.Method.Has<Initializer>() &&
                    c.Method.DefaultOverload.IsPublicInstanceWithNoSpecialName &&
                    c.Method.DefaultOverload.AllParametersAreApiInput(),
                attribute: c => new ApiAction(),
                order: Order.At.Max
            );
            conventions.SetParameterAttribute(
                when: c => c.Parameter.IsApiInput,
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
                when: (_, action) => action.HasBody,
                attribute: action => action.AdditionalAttributes.Add("Consumes(\"application/json\")"),
                order: Order.At.Infra + 10
            );
            conventions.EditMethodAttribute<ApiAction>(
                when: (_, action) => !action.ReturnIsVoid,
                attribute: action => action.AdditionalAttributes.Add("Produces(\"application/json\")"),
                order: Order.At.Infra + 10
            );
            conventions.Add(new UseDocumentationAsDescriptionConvention(_tagDescriptions, _examples), order: Order.At.Infra + 10);
            conventions.EditMethodAttribute<ApiAction>((action, context) =>
                action.AdditionalAttributes.Add($"{typeof(MappedMethodAttribute).FullName}(\"{context.Type.FullName}\", \"{context.Method.Name}\")"),
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
            files.AddAsJson(_tagDescriptions);
            files.AddAsJson(_examples);
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
    }
}