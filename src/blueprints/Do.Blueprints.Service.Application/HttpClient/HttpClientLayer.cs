using Do.Architecture;
using Microsoft.Extensions.DependencyInjection;
using static Do.DependencyInjection.DependencyInjectionLayer;

namespace Do.HttpClient;

public class HttpClientLayer : LayerBase<AddServices>
{
    readonly Dictionary<string, HttpClientDescriptor> _httpClients = [];

    protected override PhaseContext GetContext(AddServices phase)
    {
        var services = Context.Get<IServiceCollection>();

        services.AddHttpClient();

        return phase.CreateContext(_httpClients,
            onDispose: () =>
            {
                foreach (var (key, descriptor) in _httpClients)
                {
                    services
                        .AddHttpClient(key)
                        .ConfigureHttpClient(hc =>
                        {
                            hc.BaseAddress = descriptor.BaseAddress;

                            if (descriptor.DefaultHeaders is not null)
                            {
                                foreach (var (key, value) in descriptor.DefaultHeaders)
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

