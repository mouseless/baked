using System.Net;

namespace Baked.ExceptionHandling.ProblemDetails;

public class UnhandledExceptionHandler(ExceptionHandlerSettings _settings)
    : IExceptionHandler
{
    public bool CanHandle(Exception ex) => true;
    public ExceptionInfo Handle(Exception ex) => new(
        ex,
        (int)HttpStatusCode.InternalServerError,
        _settings.ShowUnhandled
            ? $"{ex.Message}{Environment.NewLine}{ex}"
            : "An_unexpected_error_has_occured_please_contact_the_administrator"
    );
}