using Do.Communication;
using Do.Communication.Http;

namespace Do;

public static class HttpCommunicationExtensions
{
    public static HttpCommunicationFeature Http(this CommunicationConfigurator _) => new();
}