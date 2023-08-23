namespace Do.Architecture;

public interface IFeature
{
    string Id => GetType().Name;
    void Configure(LayerConfigurator configurator);
}
