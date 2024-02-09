
namespace Do.Communication.Mock;

public class ResponseGenerator
{
    public Response Generate(Request request)
    {
        throw new NotSupportedException($"{request.UrlOrPath} is not configured on stub client");
    }
}
