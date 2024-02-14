using Do.Architecture;

namespace Do.Communication.Mock;

public class MockCommunicationFeature(MockClientSetups _setups)
    : IFeature<CommunicationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureTestConfiguration(tests =>
        {
            var descriptors = new MockClientBuilder(_setups).Build();
            foreach (var descriptor in descriptors)
            {
                tests.Mocks.Add(descriptor);
            }
        });
    }
}
