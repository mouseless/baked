using Do.ExceptionHandling;
using System.Net;

namespace Do.Orm;

public class RecordNotFoundExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception ex) => ex is RecordNotFoundException;
    public ExceptionInfo Handle(Exception ex) => new((int)HttpStatusCode.NotFound, ex.Message);
}
