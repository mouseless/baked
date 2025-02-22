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
            builder.Index.Type.Add<ControllerModel>();
            builder.Index.Type.Add<ApiInputAttribute>();
            builder.Index.Method.Add<ActionModel>();
            builder.Index.Parameter.Add<ParameterModel>();

            builder.Conventions.AddTypeMetadata(new ControllerModel(),
                when: c =>
                  c.Type.Has<ServiceAttribute>() &&
                  c.Type.IsClass &&
                  !c.Type.IsAbstract &&
                  !c.Type.IsGenericType &&
                  c.Type.TryGetMembers(out var members) &&
                  members.Methods.Any(m => m.DefaultOverload.IsPublicInstanceWithNoSpecialName())
            );
            builder.Conventions.AddMethodMetadata(
                attribute: c => new ActionModel(nameof(HttpMethod.Post), [c.Type.Name, c.Method.Name], "target"),
                when: c =>
                    !c.Method.Has<ExternalAttribute>() &&
                    !c.Method.Has<InitializerAttribute>() &&
                    c.Method.DefaultOverload.IsPublicInstanceWithNoSpecialName() &&
                    c.Method.DefaultOverload.AllParametersAreApiInput(),
                order: int.MaxValue
            );
            builder.Conventions.AddParameterMetadata(
                attribute: c => new ParameterModel(ParameterModelFrom.BodyOrForm),
                when: c => c.Parameter.IsApiInput()
            );
            builder.Conventions.Add(new InitApiModelConvention(), order: int.MinValue);
            builder.Conventions.Add(new AddTargetParameterConvention("target"), order: int.MinValue);

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
            builder.Conventions.Add(new ConsumesJsonConvention(_when: c => c.Action.HasBody), order: 10);
            builder.Conventions.Add(new ProducesJsonConvention(_when: c => !c.Action.Return.IsVoid), order: 10);
            builder.Conventions.Add(new UseDocumentationAsDescriptionConvention(_tagDescriptions), order: 10);
            builder.Conventions.Add(new AddMappedMethodAttributeConvention());
        });

        configurator.ConfigureApiModel(api =>
        {
            api.Usings.Add("Swashbuckle.AspNetCore.Annotations");

            configurator.UsingDomainModel(domain =>
            {
                foreach (var type in domain.Types.Having<ApiServiceAttribute>())
                {
                    if (type.FullName is null) { continue; }

                    var controller = new ControllerModel(type) { ClassName = type.CSharpFriendlyFullName.Split('.').Skip(1).Join('_') };
                    foreach (var method in type.GetMembers().Methods.Having<ApiMethodAttribute>())
                    {
                        controller.AddAction(type, method);

                        var typeExample = new RequestResponseExampleData(
                            type.GetMembers().Documentation.GetExampleCode("request"),
                            type.GetMembers().Documentation.GetExampleCode("response")
                        );

                        var methodExample = new RequestResponseExampleData(
                            method.Documentation.GetExampleCode("request"),
                            method.Documentation.GetExampleCode("response")
                        );

                        _examples.TryAdd($"{type.FullName}", typeExample);
                        _examples.TryAdd($"{type.FullName}.{method.Name}", methodExample);
                    }

                    if (!controller.Action.Any()) { continue; }

                    api.Controller.Add(controller.Id, controller);
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