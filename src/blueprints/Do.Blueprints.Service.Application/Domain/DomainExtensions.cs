using Do.Architecture;
using Do.Domain;

namespace Do;

public static class DomainExtensions
{
    public static void AddDomain(this ICollection<ILayer> layers) => layers.Add(new DomainLayer());
    public static void ConfigureDomainBuilder(this LayerConfigurator configurator, Action<DomainBuilderConfiguration> configuration) => configurator.Configure(configuration);
    public static void ConfigureDomainDescriptor(this LayerConfigurator configurator, Action<DomainDescriptor> configuration) => configurator.Configure(configuration);
}
