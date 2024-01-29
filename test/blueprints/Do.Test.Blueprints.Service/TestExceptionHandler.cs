using Do.ExceptionHandling;

namespace Do.Test;

public class TestExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception ex) => ex is TestServiceHandledException;

    public ExceptionInfo Handle(Exception ex)
    {
        var httpRequestException = (HttpRequestException)ex;

        return new(ex, (int)httpRequestException.StatusCode.GetValueOrDefault(), httpRequestException.Message);
    }
}
