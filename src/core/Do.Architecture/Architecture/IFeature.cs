namespace Do.Architecture;

public interface IFeature
{
    string Id { get; }
    void Configure(LayerConfigurator configurator);
}
