using System.Runtime.CompilerServices;

namespace Baked.Architecture;

public abstract class FeatureConfigurator<TSelf>
    where TSelf : FeatureConfigurator<TSelf>
{
    static readonly string _abstraction = TrimSuffix(
        typeof(TSelf).DeclaringType?.Name ??
        throw new InvalidOperationException($"`{typeof(TSelf).Name}` should be nested in its abstraction class, e.g. `CoreFeature.Configurator`"),
        nameof(Feature)
    );

    static string TrimSuffix(string name, string suffix) =>
        name.EndsWith(suffix) ? name[..^suffix.Length] : name;

    public IFeature<TSelf> Define(Action<LayerConfigurator> configure,
        [CallerMemberName] string id = ""
    ) => Define(id, configure);

    public IFeature<TSelf> Define(string id, Action<LayerConfigurator> configure) =>
        new Feature<TSelf>($"{_abstraction}.{id}", configure);

    public IFeature<TSelf> Disabled() =>
        Define(configurator => { });
}