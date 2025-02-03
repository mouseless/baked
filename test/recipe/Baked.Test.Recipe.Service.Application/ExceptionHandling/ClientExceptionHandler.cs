using Baked.Communication;
using Baked.ExceptionHandling;
using Newtonsoft.Json;
using System.Net;

namespace Baked.Test.ExceptionHandling;

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