using Baked.Architecture;
using Baked.Business;
using Baked.Lifetime;
using Baked.RestApi;

namespace Baked.CodingStyle.Initializable;

public class InitializableCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.SetTypeAttribute(
                attribute: () => new TransientAttribute(),
                when: c =>
                    c.Type.IsClass && !c.Type.IsAbstract &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Has<ServiceAttribute>() &&
                    members.Methods.Contains("With")
            );
            builder.Conventions.SetMethodAttribute(
                attribute: () => new InitializerAttribute(),
                when: c => c.Method.Name == "With"
            );

            builder.Conventions.Add(new AddInitializerParametersToQueryConvention());
            builder.Conventions.Add(new TargetUsingInitializerConvention(), order: RestApiLayer.MaxConventionOrder - 20);
            builder.Conventions.Add(new RemoveInitializerNameFromRouteConvention(), order: RestApiLayer.MaxConventionOrder);
        });
    }
}