using Do.Architecture;
using Microsoft.Extensions.DependencyInjection;
using static Do.DependencyInjection.DependencyInjectionLayer;

namespace Do.HttpClient;

public class HttpClientLayer : LayerBase<AddServices>
{
    readonly List<HttpClientDescriptor> _httpClients = [];

    protected override PhaseContext GetContext(AddServices phase)
    {
        var services = Context.Get<IServiceCollection>();

        services.AddHttpClient();

        return phase.CreateContext(_httpClients,
            onDispose: () =>
            {
                foreach (var client in _httpClients)
                {
                    services
                        .AddHttpClient(client.Name)
                        .ConfigureHttpClient(hc =>
                        {
                            hc.BaseAddress = client.BaseAddress;

                            if (client.DefaultHeaders is not null)
                            {
                                foreach (var (key, value) in client.DefaultHeaders)
                                {
                                    hc.DefaultRequestHeaders.Add(key, value);
                                }
                            }
                        });
                }
            }
        );
    }
}

