using Baked.Architecture;
using Baked.Business;
using Baked.Lifetime;

namespace Baked.CodingStyle.WithMethod;

public class WithMethodCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddTypeMetadata(new TransientAttribute(),
                when: c =>
                    c.Type.IsClass && !c.Type.IsAbstract &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Has<ServiceAttribute>() &&
                    members.Methods.Contains("With")
            );
            builder.Conventions.AddMethodMetadata(new InitializerAttribute(),
                when: c => c.Method.Name == "With"
            );
        });
    }
}