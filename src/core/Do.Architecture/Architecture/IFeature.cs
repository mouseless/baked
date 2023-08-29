namespace Do.Architecture;

public interface IFeature
{
    public string Id => GetType().Name;

    void Configure(LayerConfigurator configurator);
}

public interface IFeature<T> : IFeature { }
