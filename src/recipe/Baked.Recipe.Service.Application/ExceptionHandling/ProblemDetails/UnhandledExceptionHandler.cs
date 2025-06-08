using System.Net;

namespace Baked.ExceptionHandling.ProblemDetails;

public class UnhandledExceptionHandler(ExceptionHandlerSettings _settings)
    : IExceptionHandler
{
    public bool CanHandle(Exception ex) => true;
    public ExceptionInfo Handle(Exception ex) =>
        new(
            ex,
            (int)HttpStatusCode.InternalServerError,
            $"An_unexpected_error_has_occured_please_contact_the_administrator{(_settings.ShowUnhandled ? "__MESSAGE__EXCEPTION" : string.Empty)}",
            _settings.ShowUnhandled
                ? new() { ["message"] = ex.Message, ["exception"] = ex.ToString() } // order important for localization
                : null
        );
}