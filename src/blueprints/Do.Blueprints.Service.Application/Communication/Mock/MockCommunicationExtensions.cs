using Do.Communication;
using Do.Communication.Mock;

namespace Do;

public static class MockCommunicationExtensions
{
    public static MockCommunicationFeature Mock(this CommunicationConfigurator _,
        Action<MockClientSetups>? defaultResponses = default
    )
    {
        MockClientSetups setups = new();
        defaultResponses?.Invoke(setups);

        return new(setups);
    }
}
