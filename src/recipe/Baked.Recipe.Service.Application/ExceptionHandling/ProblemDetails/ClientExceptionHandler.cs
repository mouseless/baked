using Baked.Communication;
using System.Net;
using System.Text.Json;

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
        try { content = JsonSerializer.Deserialize<object>(exception.Content) ?? content; }
        catch { content = exception.Content; }

        return new(
            exception,
            (int)HttpStatusCode.InternalServerError,
            exception.Message,
            exception.LKey,
            exception.LParams,
            new() { ["content"] = content }
        );
    }
}