using Do.Architecture;
using Do.Domain;
using Do.Domain.Model;

namespace Do;

public static class DomainExtensions
{
    public static void AddDomain(this ICollection<ILayer> layers) => layers.Add(new DomainLayer());

    public static DomainModel GetDomainModel(this ApplicationContext source) => source.Get<DomainModel>();

    public static void ConfigureDomainDescriptor(this LayerConfigurator configurator, Action<DomainDescriptor> configuration) => configurator.Configure(configuration);
}
