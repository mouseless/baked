using Baked.Communication;
using Baked.Communication.Http;

namespace Baked;

public static class HttpCommunicationExtensions
{
    extension(CommunicationConfigurator _)
    {
        public HttpCommunicationFeature Http() =>
            new();
    }
}