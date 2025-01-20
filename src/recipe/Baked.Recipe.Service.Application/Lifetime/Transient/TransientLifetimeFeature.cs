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

        configurator.ConfigureServiceCollection(services =>
        {
            services.AddFromAssembly(configurator.Context.GetGeneratedAssembly(nameof(TransientLifetimeFeature)));
        });

        configurator.ConfigureGeneratedAssemblyCollection(generatedAssemblies =>
        {
            var domain = configurator.Context.GetDomainModel();

            generatedAssemblies.Add(nameof(TransientLifetimeFeature),
                assembly =>
                {
                    assembly
                        .AddReferenceFrom<TransientLifetimeFeature>()
                        .AddCodes(new TransientServiceAdderTemplate(domain));

                    foreach (var entity in domain.Types.Having<TransientAttribute>())
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