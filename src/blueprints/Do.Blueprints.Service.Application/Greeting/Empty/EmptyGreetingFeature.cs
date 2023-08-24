using Do.Architecture;

namespace Do.Greeting.Empty;

public class EmptyGreetingFeature : IGreetingFeature
{
    public string Id => GetType().Name;

    public void Configure(LayerConfigurator configurator) { }
}
