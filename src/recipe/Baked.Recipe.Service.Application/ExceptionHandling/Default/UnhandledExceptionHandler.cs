using System.Net;

namespace Baked.ExceptionHandling.Default;

public class UnhandledExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception ex) => true;
    public ExceptionInfo Handle(Exception ex) => new(
        ex,
        (int)HttpStatusCode.InternalServerError,
        "An unexpected error has occured. Please contact the administrator."
    );
}