using Baked.Architecture;
using Microsoft.Extensions.DependencyInjection;

using static Baked.DependencyInjection.DependencyInjectionLayer;

namespace Baked.HttpClient;

public class HttpClientLayer : LayerBase<AddServices>
{
    public static string DefaultConfigKey = "Default";

    readonly List<HttpClientDescriptor> _httpClients = [];

    protected override PhaseContext GetContext(AddServices phase)
    {
        var services = Context.Get<IServiceCollection>();

        services.AddHttpClient();

        return phase.CreateContext(_httpClients, onDispose: () =>
        {
            foreach (var descriptor in _httpClients)
            {
                if (descriptor.Name == DefaultConfigKey)
                {
                    services
                        .ConfigureHttpClientDefaults(builder =>
                            builder.ConfigureHttpClient(client => Apply(descriptor, client))
                        );

                    continue;
                }

                services
                    .AddHttpClient(descriptor.Name)
                    .ConfigureHttpClient(client => Apply(descriptor, client));
            }
        });

        void Apply(HttpClientDescriptor descriptor, System.Net.Http.HttpClient client)
        {
            client.BaseAddress = descriptor.BaseAddress;

            if (descriptor.DefaultHeaders is null) { return; }

            foreach (var (key, value) in descriptor.DefaultHeaders)
            {
                client.DefaultRequestHeaders.Add(key, value);
            }
        }
    }
}