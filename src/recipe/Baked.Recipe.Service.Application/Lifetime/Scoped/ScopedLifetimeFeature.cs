using Baked.Architecture;

namespace Baked.Lifetime.Scoped;

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
            services.AddFromAssembly(configurator.Context.GetGeneratedAssembly(nameof(ScopedLifetimeFeature)));
        });

        configurator.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            var domain = configurator.Context.GetDomainModel();

            generatedAssemblies.Add(nameof(ScopedLifetimeFeature),
                assembly =>
                {
                    assembly
                        .AddReferenceFrom<ScopedLifetimeFeature>()
                        .AddCodes(new ScopedServiceAdderTemplate(domain));

                    foreach (var entity in domain.Types.Having<ScopedAttribute>())
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