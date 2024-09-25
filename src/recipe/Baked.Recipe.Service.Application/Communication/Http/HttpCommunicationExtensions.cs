using Baked.Communication;
using Baked.Communication.Http;

namespace Baked;

public static class HttpCommunicationExtensions
{
    public static HttpCommunicationFeature Http(this CommunicationConfigurator _) =>
        new();
}