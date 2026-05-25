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
        configurator.Domain.ConfigureDomainConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                attribute: () => new PubliclyInitializableAttribute(),
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Has<TransientAttribute>() &&
                    !members.Has<LocatableAttribute>() &&
                    members.Methods.Any(m =>
                        m.Has<InitializerAttribute>() &&
                        m.DefaultOverload.AllParametersAreApiInput() &&
                        m.DefaultOverload.IsPublicInstanceWithNoSpecialName
                    ),
                order: 40
            );
            conventions.RemoveTypeAttribute<ControllerModelAttribute>(
                when: c =>
                    c.Type.Has<TransientAttribute>() &&
                    !c.Type.Has<LocatableAttribute>() &&
                    !c.Type.Has<PubliclyInitializableAttribute>(),
                order: 40
            );

            conventions.Add(new IncludeClassDocsForActionNamesConvention(_methodNames), order: -10);
            conventions.Add(new UseClassNameInsteadOfActionNamesConvention(_methodNames), order: -10);
            conventions.Add(new RemoveFromRouteConvention(_methodNames));
            conventions.Add(new RemoveFromRouteConvention(["Sync", "Create"]));
            conventions.Add(new UseRootPathAsGroupNameForSingleMethodNonLocatablesConvention());

            conventions.Add(new NoRequestBodyForSingleEnumerableParametersConvention(
                _when: action => action.Name.StartsWith("Sync"),
                _method: HttpMethod.Put
            ), order: -10);

            conventions.Add(new NoRequestBodyForSingleEnumerableParametersConvention(
                _when: action => action.Name.StartsWith("Create"),
                _method: HttpMethod.Patch
            ), order: -10);
        });

        configurator.RestApi.ConfigureSwaggerGenOptions(swaggerGenOptions =>
        {
            configurator.Buildtime.UsingGeneratedContext(generatedContext =>
            {
                var examples = generatedContext.ReadFileAsJson<RequestResponseExamples>() ?? [];
                swaggerGenOptions.OperationFilter<XmlExamplesFromClassOperationFilter>(_methodNames, examples);
            });
        });
    }
}