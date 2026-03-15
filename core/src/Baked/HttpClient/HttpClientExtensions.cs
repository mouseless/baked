using Baked.Architecture;
using Baked.HttpClient;

namespace Baked;

public static class HttpClientExtensions
{
    public class Configurator(LayerConfigurator _configurator)
    {
        public void ConfigureHttpClients(Action<List<HttpClientDescriptor>> configuration) =>
            _configurator.Configure(configuration);
    }

    extension(LayerConfigurator configurator)
    {
        public Configurator HttpClient => new(configurator);
    }

    extension(List<ILayer> layers)
    {
        public void AddHttpClient() =>
            layers.Add(new HttpClientLayer());
    }
}