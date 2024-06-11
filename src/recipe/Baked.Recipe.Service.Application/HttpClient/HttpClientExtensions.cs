using Baked.Architecture;
using Baked.HttpClient;

namespace Baked;

public static class HttpClientExtensions
{
    public static void AddHttpClient(this List<ILayer> source) =>
        source.Add(new HttpClientLayer());

    public static void ConfigureHttpClients(this LayerConfigurator source, Action<List<HttpClientDescriptor>> configuration) =>
        source.Configure(configuration);
}