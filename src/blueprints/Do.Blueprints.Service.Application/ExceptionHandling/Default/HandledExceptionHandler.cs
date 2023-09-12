using System.Net;

namespace Do.ExceptionHandling.Default;

public class HandledExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception ex) => ex is HandledException;
    public ExceptionInfo Handle(Exception ex) => new((int)HttpStatusCode.BadRequest, ex.Message);
}
