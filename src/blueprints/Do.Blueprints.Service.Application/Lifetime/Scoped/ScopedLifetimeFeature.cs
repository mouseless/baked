using Do.Architecture;
using Do.Business.Attributes;

namespace Do.Lifetime.Scoped;

public class ScopedLifetimeFeature : IFeature<LifetimeConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<ScopedAttribute>();
        });

        configurator.ConfigureServiceCollection(services =>
        {
            var domainModel = configurator.Context.GetDomainModel();
            foreach (var type in domainModel.Types.Having<ScopedAttribute>())
            {
                type.Apply(t =>
                {
                    services.AddScopedWithFactory(t);
                    type.GetInheritance().Interfaces
                        .Where(i => i.Model.TryGetMetadata(out var metadata) && metadata.Has<ServiceAttribute>())
                        .Apply(i => services.AddScopedWithFactory(i, t));
                });
            }
        });
    }
}