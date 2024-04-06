namespace Do.Architecture;

public interface IFeature
{
    void Configure(LayerConfigurator configurator);

    public string Id => GetType().Name;
}

public interface IFeature<T> : IFeature { }