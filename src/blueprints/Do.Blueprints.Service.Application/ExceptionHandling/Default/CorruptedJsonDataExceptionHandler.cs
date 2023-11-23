using System.Net;
using NHibernate.Exceptions;

namespace Do.ExceptionHandling.Default;

public class CorruptedJsonDataExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception ex) =>
        ex is GenericADOException &&
        ex.InnerException is InvalidDataException;

    public ExceptionInfo Handle(Exception ex) =>
        new(
            (int)HttpStatusCode.InternalServerError,
            ex.InnerException?.Message ?? ex.Message
        );
}
