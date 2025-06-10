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
                "An_unexpected_error_has_occured_please_contact_the_administrator__MESSAGE__EXCEPTION",
                new() { ["message"] = ex.Message, ["exception"] = ex.ToString() }
            )
            : new(
                ex,
                (int)HttpStatusCode.InternalServerError,
                "An_unexpected_error_has_occured_please_contact_the_administrator"
            );
}