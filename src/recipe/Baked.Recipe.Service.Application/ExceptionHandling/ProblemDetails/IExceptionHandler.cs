namespace Baked.ExceptionHandling.ProblemDetails;

public interface IExceptionHandler
{
    public bool CanHandle(Exception ex);
    public ExceptionInfo Handle(Exception ex);
}