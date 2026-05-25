using Baked.Architecture;
using Baked.Business;
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
            builder.Index.Type.Add<ControllerModelAttribute>();
            builder.Index.Type.Add<ApiInputAttribute>();
            builder.Index.Method.Add<ActionModelAttribute>();
            builder.Index.Parameter.Add<ParameterModelAttribute>();
        });

        configurator.Domain.ConfigureConventions(conventions =>
        {
            // domain attribute mutations
            conventions.SetTypeAttribute(
                attribute: c => new ControllerModelAttribute(),
                when: c =>
                  c.Type.Has<ServiceAttribute>() &&
                  c.Type.IsClass &&
                  !c.Type.IsAbstract &&
                  !c.Type.IsGenericType &&
                  c.Type.TryGetMembers(out var members) &&
                  members.Methods.Any(m => m.DefaultOverload.IsPublicInstanceWithNoSpecialName),
              order: 10
            );
            conventions.SetMethodAttribute(
                attribute: c => new ActionModelAttribute(),
                when: c =>
                    !c.Method.Has<ExternalAttribute>() &&
                    !c.Method.Has<InitializerAttribute>() &&
                    c.Method.DefaultOverload.IsPublicInstanceWithNoSpecialName &&
                    c.Method.DefaultOverload.AllParametersAreApiInput(),
                order: RestApiLayer.MaxConventionOrder
            );
            conventions.SetParameterAttribute(
                attribute: c => new ParameterModelAttribute(),
                when: c => c.Parameter.IsApiInput,
                order: RestApiLayer.MaxConventionOrder
            );

            // init before any domain convention
            conventions.Add(new InitApiModelConvention(), order: int.MinValue);
            conventions.AddMethodAttributeConfiguration<ActionModelAttribute>(
                attribute: (action, context) =>
                    action.Parameter[ParameterModelAttribute.TargetParameterName] =
                        new(ParameterModelAttribute.TargetParameterName, context.Type.CSharpFriendlyFullName, ParameterModelFrom.Services),
                order: int.MinValue
            );

            // rest api conventions
            conventions.Add(new AutoHttpMethodConvention([
                (Regexes.StartsWithGet, HttpMethod.Get),
                (Regexes.IsUpdateChangeOrSet, HttpMethod.Put),
                (Regexes.StartsWithUpdateChangeOrSet, HttpMethod.Patch),
                (Regexes.StartsWithDeleteRemoveOrClear, HttpMethod.Delete)
            ]));
            conventions.Add(new GetAndDeleteAcceptsOnlyQueryConvention());
            conventions.Add(new RemoveFromRouteConvention(["Get"]));
            conventions.Add(new RemoveFromRouteConvention(["Update", "Change", "Set"]));
            conventions.Add(new RemoveFromRouteConvention(["Delete", "Remove", "Clear"]));
            conventions.AddMethodAttributeConfiguration<ActionModelAttribute>(
                attribute: action => action.AdditionalAttributes.Add("Consumes(\"application/json\")"),
                when: (_, action) => action.HasBody,
                order: 10
            );
            conventions.AddMethodAttributeConfiguration<ActionModelAttribute>(
                attribute: action => action.AdditionalAttributes.Add("Produces(\"application/json\")"),
                when: (_, action) => !action.ReturnIsVoid,
                order: 10
            );
            conventions.Add(new UseDocumentationAsDescriptionConvention(_tagDescriptions, _examples), order: 10);
            conventions.AddMethodAttributeConfiguration<ActionModelAttribute>((action, context) =>
                action.AdditionalAttributes.Add($"{typeof(MappedMethodAttribute).FullName}(\"{context.Type.FullName}\", \"{context.Method.Name}\")")
            );
        });

        configurator.Domain.ConfigureAttributeProperties(properties =>
        {
            properties.Set<ActionModelAttribute>(action =>
            [
                new("route", Value: $"{action.Method} /{action.GetRoute()}"),
                new("form", Value: action.UseForm),
                new("flat-request-body", Value: !action.UseRequestClassForBody)
            ]);
            properties.Set<ParameterModelAttribute>(parameter =>
            [
                new("required", parameter.FromRoute || parameter.HasRequiredAttributes),
                new("in", Value: parameter.FromBodyOrForm ? null : $"{parameter.From}".Kebaberize())
            ]);
        });

        configurator.Domain.ConfigureExportConfigurations(exports =>
        {
            exports.Build("RestApi", export =>
            {
                export
                    .Include<ControllerModelAttribute>()
                    .AddFilter(controller => controller.Actions.Any())
                ;
                export.Include<ActionModelAttribute>();
                export.Include<ParameterModelAttribute>();
                export.TypeGroupName(type =>
                    type.TryGetControllerModel(out var controller) ? controller.GroupName :
                    type.Name
                );
            });
        });

        configurator.RestApi.ConfigureApiModel(api =>
        {
            api.Usings.Add("Swashbuckle.AspNetCore.Annotations");

            configurator.Domain.UsingDomainModel(domain =>
            {
                foreach (var type in domain.Types.Having<ControllerModelAttribute>())
                {
                    if (!type.TryGetMetadata(out var metadata)) { continue; }

                    var controller = metadata.Get<ControllerModelAttribute>();
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