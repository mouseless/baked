using Do.Communication;
using Do.Communication.Mock;

namespace Do;

public static class MockCommunicationExtensions
{
    public static MockCommunicationFeature Mock(this CommunicationConfigurator _,
        Action<MockClientBuilder>? configurationBuilder = default
    )
    {
        var configuration = new MockClientBuilder();
        configurationBuilder?.Invoke(configuration);

        return new(configuration);
    }
}
