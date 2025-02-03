using Baked.Architecture;
using Baked.Business;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Lifetime.Transient;

public class TransientLifetimeFeature : IFeature<LifetimeConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<TransientAttribute>();
        });

        configurator.ConfigureDomainServicesModel(model =>
        {
            var domain = configurator.Context.GetDomainModel();
            foreach (var transient in domain.Types.Having<TransientAttribute>())
            {
                model.Services.Add(new(
                    ServiceType: transient,
                    Lifetime: ServiceLifetime.Transient,
                    UseFactory: true,
                    Interfaces: !transient.TryGetInheritance(out var inheritance) ? [] : inheritance.Interfaces.Where(i => i.Model.TryGetMetadata(out var metadata) && metadata.Has<ServiceAttribute>()),
                    Forward: false
                ));

                transient.Apply(t => model.References.Add(t.Assembly));

                model.Usings.AddRange([
                    "Baked.Business",
                    "Baked.Runtime",
                    "Microsoft.Extensions.DependencyInjection"
                ]);
            }
        });
    }
}