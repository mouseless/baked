using Do.Architecture;

namespace Do.Communication.Mock;

public class MockCommunicationFeature(Action<DefaultResponseBuilder> _setupDefaultResponses)
    : IFeature<CommunicationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureTestConfiguration(tests =>
        {
            var builder = new DefaultResponseBuilder();
            _setupDefaultResponses(builder);

            foreach (var descriptor in builder.BuildMockClients())
            {
                tests.Mocks.Add(descriptor);
            }
        });
    }
}