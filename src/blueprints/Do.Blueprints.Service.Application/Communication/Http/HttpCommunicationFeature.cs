using Do.Architecture;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Communication.Http;

public class HttpCommunicationFeature : IFeature<CommunicationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(serviceCollection =>
        {
            serviceCollection.AddSingleton(typeof(ClientFactory<>));
            serviceCollection.AddSingleton(typeof(IClient<>), typeof(Client<>));
        });
    }
}

