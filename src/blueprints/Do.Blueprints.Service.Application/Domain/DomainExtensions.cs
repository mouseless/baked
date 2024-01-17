using Do.Architecture;
using Do.Domain;
using Do.Domain.Model;
using System.Reflection;

namespace Do;

public static class DomainExtensions
{
    public static void AddDomain(this ICollection<ILayer> layers) => layers.Add(new DomainLayer());

    public static DomainModel GetDomainModel(this ApplicationContext source) => source.Get<DomainModel>();

    public static void ConfigureAssemblyCollection(this LayerConfigurator configurator, Action<IAssemblyCollection> configuration) => configurator.Configure(configuration);
    public static void ConfigureTypeCollection(this LayerConfigurator configurator, Action<ITypeCollection> configuration) => configurator.Configure(configuration);
    public static void ConfigureDomainOptions(this LayerConfigurator configurator, Action<DomainBuilderOptions> configuration) => configurator.Configure(configuration);

    public static void Add(this IAssemblyCollection source, Assembly assembly) => source.Add(new(assembly));
    public static void Add<T>(this ITypeCollection source) => source.Add(typeof(T));
    public static void Add(this ITypeCollection source, Type type) => source.Add(new(type));
}
