namespace Baked.Architecture;

internal class Feature<T>(string _id, Action<LayerConfigurator> _configure)
    : IFeature<T>
{
    public string Id => _id;

    public void Configure(LayerConfigurator configurator) =>
        _configure(configurator);
}