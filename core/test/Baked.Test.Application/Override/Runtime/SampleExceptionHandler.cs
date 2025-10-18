using Baked.ExceptionHandling;
using Baked.Test.ExceptionHandling;
using System.Net;

namespace Baked.Test.Override.Runtime;

public class SampleExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception ex) => ex is SampleException;
    public ExceptionInfo Handle(Exception ex) => new(ex, (int)HttpStatusCode.BadRequest, ex.Message);
}