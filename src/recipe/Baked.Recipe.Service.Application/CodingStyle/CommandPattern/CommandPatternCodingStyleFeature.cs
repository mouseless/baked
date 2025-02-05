using Baked.Architecture;
using Baked.Business;
using Baked.Lifetime;
using Baked.RestApi.Conventions;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.CodingStyle.CommandPattern;

public class CommandPatternCodingStyleFeature(IEnumerable<string> _methodNames)
    : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddTypeMetadata(new PubliclyInitializableAttribute(),
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
            builder.Conventions.RemoveTypeMetadata<ApiServiceAttribute>(
                when: c =>
                    c.Type.Has<TransientAttribute>() &&
                    !c.Type.Has<LocatableAttribute>() &&
                    !c.Type.Has<PubliclyInitializableAttribute>(),
                order: 40
            );
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            conventions.Add(new IncludeClassDocsForActionNamesConvention(_methodNames), order: -10);
            conventions.Add(new UseClassNameInsteadOfActionNamesConvention(_methodNames), order: -10);
            conventions.Add(new RemoveFromRouteConvention(_methodNames));
            conventions.Add(new RemoveFromRouteConvention(["Sync", "Create"]));
            conventions.Add(new UseRootPathAsGroupNameForSingleMethodNonLocatablesConvention());

            conventions.Add(new NoRequestBodyForSingleEnumerableParametersConvention(
                _when: c => c.Action.Name.StartsWith("Sync"),
                _method: HttpMethod.Put
            ), order: -10);

            conventions.Add(new NoRequestBodyForSingleEnumerableParametersConvention(
                _when: c => c.Action.Name.StartsWith("Create"),
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