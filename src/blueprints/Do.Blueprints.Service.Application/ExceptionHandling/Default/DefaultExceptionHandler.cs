using System.Net;

namespace Do.ExceptionHandling.Default;

public class DefaultExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception ex) => true;
    public ExceptionInfo Handle(Exception ex) => new((int)HttpStatusCode.InternalServerError, ex.Message);
}
