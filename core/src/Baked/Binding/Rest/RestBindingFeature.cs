using Baked.Architecture;
using Baked.Business;
using Baked.RestApi;
using Baked.RestApi.Conventions;
using Baked.RestApi.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Binding.Rest;

public class RestBindingFeature : IFeature<BindingConfigurator>
{
    readonly TagDescriptions _tagDescriptions = new();
    readonly RequestResponseExamples _examples = [];

    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            // domain metadata indices
            builder.Index.Type.Add<ControllerModelAttribute>();
            builder.Index.Type.Add<ApiInputAttribute>();
            builder.Index.Method.Add<ActionModelAttribute>();
            builder.Index.Parameter.Add<ParameterModelAttribute>();

            // domain metadata mutations
            builder.Conventions.SetTypeMetadata(
                attribute: c => new ControllerModelAttribute(),
                when: c =>
                  c.Type.Has<ServiceAttribute>() &&
                  c.Type.IsClass &&
                  !c.Type.IsAbstract &&
                  !c.Type.IsGenericType &&
                  c.Type.TryGetMembers(out var members) &&
                  members.Methods.Any(m => m.DefaultOverload.IsPublicInstanceWithNoSpecialName()),
              order: 10
            );
            builder.Conventions.SetMethodMetadata(
                attribute: c => new ActionModelAttribute(),
                when: c =>
                    !c.Method.Has<ExternalAttribute>() &&
                    !c.Method.Has<InitializerAttribute>() &&
                    c.Method.DefaultOverload.IsPublicInstanceWithNoSpecialName() &&
                    c.Method.DefaultOverload.AllParametersAreApiInput(),
                order: RestApiLayer.MaxConventionOrder
            );
            builder.Conventions.SetParameterMetadata(
                attribute: c => new ParameterModelAttribute(),
                when: c => c.Parameter.IsApiInput(),
                order: RestApiLayer.MaxConventionOrder
            );

            // init before any domain convention
            builder.Conventions.Add(new InitApiModelConvention(), order: int.MinValue);
            builder.Conventions.AddMethodMetadataConfiguration<ActionModelAttribute>(
                attribute: (action, context) =>
                    action.Parameter[ParameterModelAttribute.TargetParameterName] =
                        new(ParameterModelAttribute.TargetParameterName, context.Type.CSharpFriendlyFullName, ParameterModelFrom.Services),
                order: int.MinValue
            );

            // rest api conventions
            builder.Conventions.Add(new AutoHttpMethodConvention([
                (Regexes.StartsWithGet, HttpMethod.Get),
                (Regexes.IsUpdateChangeOrSet, HttpMethod.Put),
                (Regexes.StartsWithUpdateChangeOrSet, HttpMethod.Patch),
                (Regexes.StartsWithDeleteRemoveOrClear, HttpMethod.Delete)
            ]));
            builder.Conventions.Add(new GetAndDeleteAcceptsOnlyQueryConvention());
            builder.Conventions.Add(new RemoveFromRouteConvention(["Get"]));
            builder.Conventions.Add(new RemoveFromRouteConvention(["Update", "Change", "Set"]));
            builder.Conventions.Add(new RemoveFromRouteConvention(["Delete", "Remove", "Clear"]));
            builder.Conventions.AddMethodMetadataConfiguration<ActionModelAttribute>(
                attribute: action => action.AdditionalAttributes.Add("Consumes(\"application/json\")"),
                when: (_, action) => action.HasBody,
                order: 10
            );
            builder.Conventions.AddMethodMetadataConfiguration<ActionModelAttribute>(
                attribute: action => action.AdditionalAttributes.Add("Produces(\"application/json\")"),
                when: (_, action) => !action.ReturnIsVoid,
                order: 10
            );
            builder.Conventions.Add(new UseDocumentationAsDescriptionConvention(_tagDescriptions, _examples), order: 10);
            builder.Conventions.AddMethodMetadataConfiguration<ActionModelAttribute>((action, context) =>
                action.AdditionalAttributes.Add($"{typeof(MappedMethodAttribute).FullName}(\"{context.Type.FullName}\", \"{context.Method.Name}\")")
            );
        });

        configurator.ConfigureApiModel(api =>
        {
            api.Usings.Add("Swashbuckle.AspNetCore.Annotations");

            configurator.UsingDomainModel(domain =>
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

        configurator.ConfigureGeneratedFileCollection(files =>
        {
            files.AddAsJson(_tagDescriptions);
            files.AddAsJson(_examples);
        });

        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
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

            configurator.UsingGeneratedContext(generatedContext =>
            {
                var tagDescriptions = generatedContext.ReadFileAsJson<TagDescriptions>() ?? [];
                swaggerGenOptions.DocumentFilter<ApplyTagDescriptionsDocumentFilter>(tagDescriptions);

                var examples = generatedContext.ReadFileAsJson<RequestResponseExamples>() ?? [];
                swaggerGenOptions.OperationFilter<XmlExamplesOperationFilter>(examples);
            });
        });
    }
}