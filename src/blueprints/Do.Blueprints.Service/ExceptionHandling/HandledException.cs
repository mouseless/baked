using System.Net;

namespace Do.ExceptionHandling;

public abstract class HandledException : Exception
{
    public virtual Dictionary<string, object?> ExtraData { get; private set; }
    public virtual HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    protected HandledException(string message, Dictionary<string, object?>? extraData = default) : base(message)
    {
        ExtraData = extraData ?? [];
    }

    protected HandledException(string message, Exception innerException, Dictionary<string, object?>? extraData = default) : base(message, innerException)
    {
        ExtraData = extraData ?? [];
    }
}
