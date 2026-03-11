using Baked.Architecture;
using Baked.HttpClient;

namespace Baked;

public static class HttpClientExtensions
{
    extension(List<ILayer> layers)
    {
        public void AddHttpClient() =>
            layers.Add(new HttpClientLayer());
    }

    extension(LayerConfigurator configurator)
    {
        public void ConfigureHttpClients(Action<List<HttpClientDescriptor>> configuration) =>
            configurator.Configure(configuration);
    }

}