namespace Do.Architecture;

public sealed class Feature
{
    public static readonly IFeature Empty = new EmptyFeature();

    class EmptyFeature : IFeature
    {
        public void Configure(LayerConfigurator configurator) { }
    }
}
