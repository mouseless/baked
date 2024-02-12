using Do.ExceptionHandling;
using Newtonsoft.Json;
using System.Net;

namespace Do.Test;

public class HttpRequestExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception ex) => ex is HttpRequestException;

    public ExceptionInfo Handle(Exception ex)
    {
        var exception = (HttpRequestException)ex;
        var statusCode = (int)(exception.StatusCode ?? HttpStatusCode.InternalServerError);
        var message = exception.Message;
        var inner = exception.InnerException;

        if (inner is not null)
        {
            var content = JsonConvert.DeserializeObject<dynamic>(inner.Message);
            message = $"{content?.detail.Value}";
        }

        return new(exception, statusCode, message);
    }
}
