using Baked.Architecture;

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
                model.Services.AddTransient(transient, useFactory: true);

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