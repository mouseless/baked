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

        configurator.ConfigureDomainServiceCollection(services =>
        {
            var domain = configurator.Context.GetDomainModel();
            foreach (var singleton in domain.Types.Having<SingletonAttribute>())
            {
                services.AddSingleton(singleton, forward: true);

                singleton.Apply(t => services.References.Add(t.Assembly));

                services.Usings.AddRange([
                    "Baked.Business",
                    "Baked.Runtime",
                    "Microsoft.Extensions.DependencyInjection"
                ]);
            }
        });
    }
}