
using System.Net;

namespace Do.ExceptionHandling.Default;

public class UnauthorizedAccessExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception ex) => ex is UnauthorizedAccessException;
    public ExceptionInfo Handle(Exception ex) => new(ex, (int)HttpStatusCode.Unauthorized, ex.Message);
}