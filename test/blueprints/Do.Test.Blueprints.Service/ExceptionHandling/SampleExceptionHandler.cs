using Do.ExceptionHandling;
using System.Net;

namespace Do.Test.ExceptionHandling;

public class SampleExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception ex) => ex is SampleException;
    public ExceptionInfo Handle(Exception ex) => new(ex, (int)HttpStatusCode.BadRequest, ex.Message);
}
