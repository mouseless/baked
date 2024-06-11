using Baked.Architecture;
using Baked.Business;

namespace Baked.Lifetime.Transient;

public class TransientLifetimeFeature : IFeature<LifetimeConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<TransientAttribute>();
        });

        configurator.ConfigureServiceCollection(services =>
        {
            var domainModel = configurator.Context.GetDomainModel();
            foreach (var type in domainModel.Types.Having<TransientAttribute>())
            {
                type.Apply(t =>
                {
                    services.AddTransientWithFactory(t);
                    type.GetInheritance().Interfaces
                        .Where(i => i.Model.TryGetMetadata(out var metadata) && metadata.Has<ServiceAttribute>())
                        .Apply(i => services.AddTransientWithFactory(i, t));
                });
            }
        });
    }
}