using Baked.Architecture;

namespace Baked.Communication.Mock;

public class MockCommunicationFeature(Action<DefaultResponseBuilder> _setupDefaultResponses)
    : IFeature<CommunicationFeature.Configurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Testing.ConfigureTestConfiguration(tests =>
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