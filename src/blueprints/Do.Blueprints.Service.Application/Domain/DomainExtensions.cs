using Do.Architecture;
using Do.Domain;

namespace Do;

public static class DomainExtensions
{
    public static void AddDomain(this ICollection<ILayer> layers) => layers.Add(new DomainLayer());
    public static void ConfigureDomain(this LayerConfigurator configurator, Action<DomainConfiguration> configuration) => configurator.Configure(configuration);
}
