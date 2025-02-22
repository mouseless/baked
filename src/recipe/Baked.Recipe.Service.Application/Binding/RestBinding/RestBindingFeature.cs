using Baked.Architecture;
using Baked.Binding;

namespace Baked.Business.DomainAssemblies;

public class RestBindingFeature : IFeature<BindingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.Add(new InitApiModelConvention(), order: int.MinValue);
        });
    }
}