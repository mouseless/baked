using Do.Architecture;
using Do.HttpClient;

namespace Do;

public static class HttpClientExtensions
{
    public static void AddHttpClient(this List<ILayer> source) =>
        source.Add(new HttpClientLayer());
    public static void ConfigureHttpClients(this LayerConfigurator source, Action<Dictionary<string, HttpClientDescriptor>> configuration) =>
        source.Configure(configuration);
}
