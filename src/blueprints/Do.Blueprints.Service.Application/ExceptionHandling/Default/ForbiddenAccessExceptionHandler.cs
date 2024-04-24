using Do.Authorization;
using System.Net;

namespace Do.ExceptionHandling.Default;

public class ForbiddenAccessExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception ex) => ex is ForbiddenAccessException;
    public ExceptionInfo Handle(Exception ex) => new(ex, (int)HttpStatusCode.Forbidden, ex.Message);
}