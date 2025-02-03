using Baked.Architecture;
using Baked.Business;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Lifetime.Singleton;

public class SingletonLifetimeFeature : IFeature<LifetimeConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<SingletonAttribute>();
        });

        configurator.ConfigureDomainServicesModel(model =>
        {
            var domain = configurator.Context.GetDomainModel();
            foreach (var singleton in domain.Types.Having<SingletonAttribute>())
            {
                model.Services.Add(new(
                    ServiceType: singleton,
                    Lifetime: ServiceLifetime.Singleton,
                    UseFactory: false,
                    Interfaces: !singleton.TryGetInheritance(out var inheritance) ? [] : inheritance.Interfaces.Where(i => i.Model.TryGetMetadata(out var metadata) && metadata.Has<ServiceAttribute>()),
                    Forward: true
                ));

                singleton.Apply(t => model.References.Add(t.Assembly));

                model.Usings.AddRange([
                    "Baked.Business",
                    "Baked.Runtime",
                    "Microsoft.Extensions.DependencyInjection"
                ]);
            }
        });
    }
}