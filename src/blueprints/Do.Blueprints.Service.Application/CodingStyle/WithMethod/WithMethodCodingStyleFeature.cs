using Do.Architecture;
using Do.Business.Attributes;
using Do.Lifetime;

namespace Do.CodingStyle.WithMethod;

public class WithMethodCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddType(new TransientAttribute(),
                when: type =>
                    type.IsClass && !type.IsAbstract &&
                    type.TryGetMembers(out var members) &&
                    members.Has<ServiceAttribute>() &&
                    members.Methods.Contains("With")
            );
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            conventions.Add(new EntityPublicWithIsPostResourceConvention());
        });
    }
}