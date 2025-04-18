using Baked.Communication;
using Newtonsoft.Json;
using System.Net;

namespace Baked.ExceptionHandling.ProblemDetails;

public class ClientExceptionHandler(ExceptionHandlerSettings _settings)
    : IExceptionHandler
{
    public bool CanHandle(Exception ex) => _settings.ShowUnhandled && ex is ClientException;

    public ExceptionInfo Handle(Exception ex)
    {
        var exception = (ClientException)ex;

        if (string.IsNullOrWhiteSpace(exception.Content))
        {
            return new(exception, (int)HttpStatusCode.InternalServerError, exception.Message);
        }

        object content = new { };
        try { content = JsonConvert.DeserializeObject<object>(exception.Content) ?? content; }
        catch { content = exception.Content; }

        return new(
            exception,
            (int)HttpStatusCode.InternalServerError,
            exception.Message,
            new() { ["Content"] = content }
        );
    }
}