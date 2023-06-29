using Do.Architecture;
using Do.Test.Blueprints.Service.DependencyInjection;

namespace Do;

public static class DependencyInjectionExtensions
{
    public static void AddDependencyInjection(this ICollection<ILayer> layers) => layers.Add(new DependencyInjectionLayer());
    public static void ConfigureServiceCollection(this ConfigurationTarget target, Action<IServiceCollection> configuration) => target.Apply(configuration);
}
