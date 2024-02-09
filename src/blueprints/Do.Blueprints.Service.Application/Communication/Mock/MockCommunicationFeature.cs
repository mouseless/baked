using Do.Architecture;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Communication.Mock;

public class MockCommunicationFeature : IFeature<CommunicationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(serviceCollection =>
        {
            serviceCollection.AddSingleton<ResponseGenerator>();
            serviceCollection.AddSingleton(typeof(IClient<>), typeof(Client<>));
        });
    }
}
