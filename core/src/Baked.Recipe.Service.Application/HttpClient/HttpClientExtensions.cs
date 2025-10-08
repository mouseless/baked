using Baked.Architecture;
using Baked.HttpClient;

namespace Baked;

public static class HttpClientExtensions
{
    public static void AddHttpClient(this List<ILayer> layers) =>
        layers.Add(new HttpClientLayer());

    public static void ConfigureHttpClients(this LayerConfigurator configurator, Action<List<HttpClientDescriptor>> configuration) =>
        configurator.Configure(configuration);
}