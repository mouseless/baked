using System.Net;

namespace Do.ExceptionHandling.Default;

public class UnhandledExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception ex) => true;
    public ExceptionInfo Handle(Exception ex) => new(
        (int)HttpStatusCode.InternalServerError,
        "An unexpected error has occured. Please contact the administrator."
    );
}
