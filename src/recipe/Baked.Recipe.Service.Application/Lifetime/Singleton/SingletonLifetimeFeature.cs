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

        configurator.ConfigureServiceCollection(services =>
        {
            services.AddFromAssembly(configurator.Context.GetGeneratedAssembly(nameof(SingletonLifetimeFeature)));
        });

        configurator.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            var domain = configurator.Context.GetDomainModel();

            generatedAssemblies.Add(nameof(SingletonLifetimeFeature),
                assembly =>
                {
                    assembly
                        .AddReferenceFrom<SingletonLifetimeFeature>()
                        .AddCodes(new SingletonServiceAdderTemplate(domain));

                    foreach (var entity in domain.Types.Having<SingletonAttribute>())
                    {
                        entity.Apply(t => assembly.AddReferenceFrom(t));
                    }
                },
                compilationOptions => compilationOptions.WithUsings(
                    "Baked",
                    "Baked.Business",
                    "Baked.Runtime",
                    "Microsoft.Extensions.DependencyInjection",
                    "System",
                    "System.Linq",
                    "System.Collections",
                    "System.Collections.Generic",
                    "System.Threading.Tasks"
                )
            );
        });
    }
}