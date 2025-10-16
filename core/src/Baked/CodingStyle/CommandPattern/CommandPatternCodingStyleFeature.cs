using Baked.Architecture;
using Baked.Binding;
using Baked.Business;
using Baked.Lifetime;
using Baked.RestApi.Conventions;
using Baked.RestApi.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.CodingStyle.CommandPattern;

public class CommandPatternCodingStyleFeature(IEnumerable<string> _methodNames)
    : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.SetTypeAttribute(new PubliclyInitializableAttribute(),
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Has<TransientAttribute>() &&
                    !members.Has<LocatableAttribute>() &&
                    members.Methods.Any(m =>
                        m.Has<InitializerAttribute>() &&
                        m.DefaultOverload.AllParametersAreApiInput() &&
                        m.DefaultOverload.IsPublicInstanceWithNoSpecialName()
                    ),
                order: 40
            );
            builder.Conventions.RemoveTypeAttribute<ControllerModelAttribute>(
                when: c =>
                    c.Type.Has<TransientAttribute>() &&
                    !c.Type.Has<LocatableAttribute>() &&
                    !c.Type.Has<PubliclyInitializableAttribute>(),
                order: 40
            );

            builder.Conventions.Add(new IncludeClassDocsForActionNamesConvention(_methodNames), order: -10);
            builder.Conventions.Add(new UseClassNameInsteadOfActionNamesConvention(_methodNames), order: -10);
            builder.Conventions.Add(new RemoveFromRouteConvention(_methodNames));
            builder.Conventions.Add(new RemoveFromRouteConvention(["Sync", "Create"]));
            builder.Conventions.Add(new UseRootPathAsGroupNameForSingleMethodNonLocatablesConvention());

            builder.Conventions.Add(new NoRequestBodyForSingleEnumerableParametersConvention(
                _when: action => action.Name.StartsWith("Sync"),
                _method: HttpMethod.Put
            ), order: -10);

            builder.Conventions.Add(new NoRequestBodyForSingleEnumerableParametersConvention(
                _when: action => action.Name.StartsWith("Create"),
                _method: HttpMethod.Patch
            ), order: -10);
        });

        configurator.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            configurator.UsingGeneratedContext(generatedContext =>
            {
                var examples = generatedContext.ReadFileAsJson<RequestResponseExamples>() ?? [];
                swaggerGenOptions.OperationFilter<XmlExamplesFromClassOperationFilter>(_methodNames, examples);
            });
        });
    }
}