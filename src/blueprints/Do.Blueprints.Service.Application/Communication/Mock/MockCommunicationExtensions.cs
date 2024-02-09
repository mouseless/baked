using Do.Communication;
using Do.Communication.Mock;

namespace Do;

public static class MockCommunicationExtensions
{
    public static MockCommunicationFeature Mock(this CommunicationConfigurator _) => new();
}
