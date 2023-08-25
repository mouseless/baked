namespace Do.Architecture;

public sealed class Feature
{
    public static IFeature<T> Empty<T>() => new EmptyFeature<T>();

    class EmptyFeature<T> : IFeature<T>
    {
        public string Id => GetType().Name;

        public void Configure(LayerConfigurator configurator) { }
    }
}
