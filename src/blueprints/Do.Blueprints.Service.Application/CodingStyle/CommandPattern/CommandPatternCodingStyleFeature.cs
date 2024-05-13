using Do.Architecture;
using Do.Business;
using Do.Lifetime;
using Do.RestApi.Conventions;

namespace Do.CodingStyle.CommandPattern;

public class CommandPatternCodingStyleFeature : IFeature<CodingStyleConfigurator>
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
                    members
                        .Methods.SingleOrDefault(m => m.Has<InitializerAttribute>())
                        ?.DefaultOverload.IsPublicInstanceWithNoSpecialName() == true,
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
            conventions.Add(new InitializeUsingQueryParametersConvention(), order: -30);
            conventions.Add(new UseClassNameInsteadOfActionNames(["Execute", "Process"]), order: -20);
            conventions.Add(new RemoveFromRouteConvention(["Execute", "Process"]));
            conventions.Add(new UseRootPathAsGroupNameForSingleMethodNonLocatables());
        });
    }
}