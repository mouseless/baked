using Baked.Architecture;
using Baked.Business;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Lifetime.Scoped;

public class ScopedLifetimeFeature : IFeature<LifetimeConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<ScopedAttribute>();
        });

        configurator.ConfigureDomainServicesModel(model =>
        {
            var domain = configurator.Context.GetDomainModel();
            foreach (var scoped in domain.Types.Having<ScopedAttribute>())
            {
                model.Services.Add(new(
                    ServiceType: scoped,
                    Lifetime: ServiceLifetime.Scoped,
                    UseFactory: true,
                    Interfaces: !scoped.TryGetInheritance(out var inheritance) ? [] : inheritance.Interfaces.Where(i => i.Model.TryGetMetadata(out var metadata) && metadata.Has<ServiceAttribute>()),
                    Forward: false
                ));

                scoped.Apply(t => model.References.Add(t.Assembly));

                model.Usings.AddRange([
                    "Baked.Business",
                    "Baked.Runtime",
                    "Microsoft.Extensions.DependencyInjection"
                ]);
            }
        });
    }
}