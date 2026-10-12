namespace Baked.Architecture;

public sealed class Feature
{
    public static IFeature<T> Empty<T>() => new EmptyFeature<T>();

    class EmptyFeature<T> : IFeature<T>
    {
        public string Id => $"{nameof(EmptyFeature<>)}<{typeof(T).Name}>";
        public void Configure(LayerConfigurator configurator) { }
    }
}

internal class Feature<T>(string _id, Action<LayerConfigurator> _configure)
    : IFeature<T>
{
    public string Id => _id;

    public void Configure(LayerConfigurator configurator) =>
        _configure(configurator);
}