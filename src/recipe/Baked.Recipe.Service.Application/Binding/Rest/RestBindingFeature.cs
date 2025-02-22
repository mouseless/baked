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

            // domain metadata add/remove
            builder.Conventions.AddTypeMetadata(
                attribute: c => new ControllerModelAttribute(),
                when: c =>
                  c.Type.Has<ServiceAttribute>() &&
                  c.Type.IsClass &&
                  !c.Type.IsAbstract &&
                  !c.Type.IsGenericType &&
                  c.Type.TryGetMembers(out var members) &&
                  members.Methods.Any(m => m.DefaultOverload.IsPublicInstanceWithNoSpecialName()),
                order: int.MaxValue - 10
            );
            builder.Conventions.AddMethodMetadata(
                attribute: c => new ActionModelAttribute(nameof(HttpMethod.Post), [c.Type.Name, c.Method.Name], "target"),
                when: c =>
                    !c.Method.Has<ExternalAttribute>() &&
                    !c.Method.Has<InitializerAttribute>() &&
                    c.Method.DefaultOverload.IsPublicInstanceWithNoSpecialName() &&
                    c.Method.DefaultOverload.AllParametersAreApiInput(),
                order: int.MaxValue - 10
            );
            builder.Conventions.AddParameterMetadata(
                attribute: c => new ParameterModelAttribute(ParameterModelFrom.BodyOrForm),
                when: c => c.Parameter.IsApiInput(),
                order: int.MaxValue - 10
            );

            // init before any domain convention
            builder.Conventions.Add(new InitApiModelConvention(), order: int.MinValue);
            builder.Conventions.Add(new AddTargetParameterConvention("target"), order: int.MinValue);

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
            builder.Conventions.Add(new ConsumesJsonConvention(_when: action => action.HasBody), order: 10);
            builder.Conventions.Add(new ProducesJsonConvention(_when: action => !action.ReturnIsVoid), order: 10);
            builder.Conventions.Add(new UseDocumentationAsDescriptionConvention(_tagDescriptions), order: 10);
            builder.Conventions.Add(new AddMappedMethodAttributeConvention());
        });

        configurator.ConfigureApiModel(api =>
        {
            api.Usings.Add("Swashbuckle.AspNetCore.Annotations");

            configurator.UsingDomainModel(domain =>
            {
                foreach (var type in domain.Types.Having<ControllerModelAttribute>())
                {
                    if (!type.TryGetMetadata(out var metadata)) { continue; }

                    var controller = metadata.GetSingle<ControllerModelAttribute>();
                    api.Controller[controller.Id] = controller;
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