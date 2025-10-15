namespace Baked.ExceptionHandling.ProblemDetails;

public class HandledExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception ex) => ex is HandledException;
    public ExceptionInfo Handle(Exception ex) => new(ex, (int)((HandledException)ex).StatusCode, ex.Message, ((HandledException)ex).ExtraData);
}