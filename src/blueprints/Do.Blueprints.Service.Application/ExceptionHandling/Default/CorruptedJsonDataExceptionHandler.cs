using NHibernate.Exceptions;

namespace Do.ExceptionHandling.Default;

public class CorruptedJsonDataExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception ex) => ex is GenericADOException && ex.InnerException != null;
    public ExceptionInfo Handle(Exception ex) =>
        new(
            500,
            ex.InnerException?.Message ?? ex.Message
        );
}
