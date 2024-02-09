using Do.Architecture;

namespace Do.Communication.Mock;

public class MockCommunicationFeature(MockClientConfiguration configuration) : IFeature<CommunicationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureTestConfiguration(tests =>
        {
            foreach (var mockClientDescriptor in configuration.MockClientDescriptors)
            {
                tests.Mocks.Add(mockClientDescriptor);
            }
        });
    }
}
