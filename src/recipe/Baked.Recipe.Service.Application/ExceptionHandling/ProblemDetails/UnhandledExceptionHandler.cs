using System.Net;

namespace Baked.ExceptionHandling.ProblemDetails;

public class UnhandledExceptionHandler(ExceptionHandlerSettings _settings)
    : IExceptionHandler
{
    public bool CanHandle(Exception ex) => true;
    public ExceptionInfo Handle(Exception ex) =>
        _settings.ShowUnhandled
            ? new(
                ex,
                (int)HttpStatusCode.InternalServerError,
                "An unexpected error has occured, please contact the administrator: '{0}'\n{1}",
                new() { ["message"] = ex.Message, ["exception"] = ex.ToString() }
            )
            : new(
                ex,
                (int)HttpStatusCode.InternalServerError,
                "An unexpected error has occured, please contact the administrator"
            );
}