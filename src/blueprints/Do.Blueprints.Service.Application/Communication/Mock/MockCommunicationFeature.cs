using Do.Architecture;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Do.Communication.Mock;

public class MockCommunicationFeature(MockClientConfiguration _mockClientConfiguration) : IFeature<CommunicationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<Func<Response>>(() => new Response(JsonConvert.SerializeObject(_mockClientConfiguration.DefaultResponse)));
            services.AddSingleton(typeof(IClient<>), typeof(Client<>));
        });

        configurator.ConfigureTestConfiguration(tests =>
        {
            foreach (var descriptor in _mockClientConfiguration.MockClientDescriptors)
            {
                tests.Mocks.Add(descriptor);
            }
        });
    }
}
