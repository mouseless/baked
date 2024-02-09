using Do.Communication;
using Do.Communication.Mock;

namespace Do;

public static class MockCommunicationExtensions
{
    public static MockCommunicationFeature Mock(this CommunicationConfigurator _,
        Action<MockClientConfiguration>? configurationBuilder = default
    )
    {
        var configuration = new MockClientConfiguration();
        configurationBuilder?.Invoke(configuration);

        return new(configuration);
    }
}
