using Baked.Architecture;

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
                model.Services.AddSingleton(singleton, forward: true);

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