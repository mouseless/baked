using Baked.Architecture;
using Baked.Business;
using Baked.RestApi;
using Baked.RestApi.Conventions;
using Baked.RestApi.Model;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Baked.Binding.Rest;

public class RestBindingFeature : IFeature<BindingConfigurator>
{
    readonly TagDescriptions _tagDescriptions = new();
    readonly RequestResponseExamples _examples = [];

    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureAttributeDatas(datas =>
        {
            datas.Create<ControllerModelAttribute>(controller =>
            [
                new(controller.Id),
                new(controller.ClassName),
                new(controller.GroupName),
                new(controller.Action),
                new(controller.Orphan)
            ]);
            datas.Create<ActionModelAttribute>(action =>
            [
                new(action.Id),
                new(action.Name),
                new(action.Method),
                new(action.ReturnType),
                new(action.ReturnIsAsync),
                new(action.ReturnIsVoid),
                new(action.InvocationIsAsync),
                new(action.UseForm),
                new(action.UseRequestClassForBody),
                new(action.AdditionalAttributes),
                new(action.Order),
                new(action.Orphan),
                new(action.HasBody)
            ]);
            datas.Create<MappedMethodAttribute>(mappedMethod => [
                new(mappedMethod.TypeFullName),
                new(mappedMethod.MethodName),
            ]);
            datas.Create<ParameterModelAttribute>(parameter =>
            [
                new(parameter.Id),
                new(parameter.From),
                new(parameter.Type),
                new(parameter.Name),
                new(parameter.InternalName),
                new(parameter.IsOptional),
                new(parameter. DefaultValue),
                new(parameter.IsInvokeMethodParameter),
                new(parameter.IsHardCoded),
                new(parameter. Order),
                new(parameter.RoutePosition),
                new(parameter.AdditionalAttributes),
                new(parameter.Orphan)
            ]);
        });

        configurator.Domain.ConfigureAttributeExportCollection(exports =>
        {
            exports.RestApi(restApi =>
            {
                restApi.Include<ControllerModelAttribute>();
                restApi.Include<ActionModelAttribute>()
                    .AddData(action => new(Name: "Route", Value: action.GetRoute()));
                restApi.Include<ParameterModelAttribute>();

                restApi.TypeGroupName(type => type.Get<ControllerModelAttribute>().GroupName);
            });
        });

        configurator.Domain.ConfigureDomainModelBuilder(builder =>
        {
            // domain attribute indices
            builder.Index.Type.Add<ControllerModelAttribute>();
            builder.Index.Type.Add<ApiInputAttribute>();
            builder.Index.Method.Add<ActionModelAttribute>();
            builder.Index.Parameter.Add<ParameterModelAttribute>();

            // domain attribute mutations
            builder.Conventions.SetTypeAttribute(
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
            builder.Conventions.SetMethodAttribute(
                attribute: c => new ActionModelAttribute(),
                when: c =>
                    !c.Method.Has<ExternalAttribute>() &&
                    !c.Method.Has<InitializerAttribute>() &&
                    c.Method.DefaultOverload.IsPublicInstanceWithNoSpecialName &&
                    c.Method.DefaultOverload.AllParametersAreApiInput(),
                order: RestApiLayer.MaxConventionOrder
            );
            builder.Conventions.SetParameterAttribute(
                attribute: c => new ParameterModelAttribute(),
                when: c => c.Parameter.IsApiInput,
                order: RestApiLayer.MaxConventionOrder
            );

            // init before any domain convention
            builder.Conventions.Add(new InitApiModelConvention(), order: int.MinValue);
            builder.Conventions.AddMethodAttributeConfiguration<ActionModelAttribute>(
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
            builder.Conventions.AddMethodAttributeConfiguration<ActionModelAttribute>(
                attribute: action => action.AdditionalAttributes.Add("Consumes(\"application/json\")"),
                when: (_, action) => action.HasBody,
                order: 10
            );
            builder.Conventions.AddMethodAttributeConfiguration<ActionModelAttribute>(
                attribute: action => action.AdditionalAttributes.Add("Produces(\"application/json\")"),
                when: (_, action) => !action.ReturnIsVoid,
                order: 10
            );
            builder.Conventions.Add(new UseDocumentationAsDescriptionConvention(_tagDescriptions, _examples), order: 10);
            builder.Conventions.AddMethodAttributeConfiguration<ActionModelAttribute>((action, context) =>
                action.AdditionalAttributes.Add($"{typeof(MappedMethodAttribute).FullName}(\"{context.Type.FullName}\", \"{context.Method.Name}\")")
            );
        });

        configurator.Domain.ConfigureAttributeExportCollection(exports =>
        {
            exports.RestApi(restApi =>
            {
                restApi.Include<ControllerModelAttribute>();
                restApi.Include<ActionModelAttribute>();
                restApi.Include<ParameterModelAttribute>();

                restApi.TypeGroupName(type => type.Get<ControllerModelAttribute>().GroupName);
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

        configurator.CodeGeneration.ConfigureGeneratedFileCollection(files =>
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

            configurator.CodeGeneration.UsingGeneratedContext(generatedContext =>
            {
                var tagDescriptions = generatedContext.ReadFileAsJson<TagDescriptions>() ?? [];
                swaggerGenOptions.DocumentFilter<ApplyTagDescriptionsDocumentFilter>(tagDescriptions);

                var examples = generatedContext.ReadFileAsJson<RequestResponseExamples>() ?? [];
                swaggerGenOptions.OperationFilter<XmlExamplesOperationFilter>(examples);
            });
        });
    }
}