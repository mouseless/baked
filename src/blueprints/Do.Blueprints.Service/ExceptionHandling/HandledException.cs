using System.Net;

namespace Do.ExceptionHandling;

public class HandledException : Exception
{
    public virtual HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public HandledException(string message) : base(message) { }
    public HandledException(string message, Exception innerException) : base(message, innerException) { }
}
