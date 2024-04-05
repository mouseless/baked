using Do.Communication;
using Do.ExceptionHandling;
using Newtonsoft.Json;
using System.Net;

namespace Do.Test.Communication;

public class ClientExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception ex) => ex is ClientException;

    public ExceptionInfo Handle(Exception ex)
    {
        var exception = (ClientException)ex;
        var statusCode = (int)(exception.InnerException.StatusCode ?? HttpStatusCode.InternalServerError);

        if (string.IsNullOrWhiteSpace(exception.Content))
        {
            return new(exception, statusCode, exception.Message);
        }

        var content = JsonConvert.DeserializeObject<dynamic>(exception.Content);

        return new(exception, statusCode, $"{content?.detail.Value}");
    }
}
