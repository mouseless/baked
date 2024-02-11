using Do.Architecture;

namespace Do.Communication.Mock;

public class MockCommunicationFeature(MockClientConfiguration _mockClientConfiguration) : IFeature<CommunicationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureTestConfiguration(tests =>
        {
            foreach (var descriptor in _mockClientConfiguration.MockClientDescriptors)
            {
                tests.Mocks.Add(descriptor);
            }
        });
    }
}
