using System.Net;

namespace Do.ExceptionHandling;

public abstract class HandledException(string message, Exception? innerException,
    Dictionary<string, object?>? extraData = default
) : Exception(message, innerException)
{
    public virtual Dictionary<string, object?> ExtraData { get; private set; } = extraData ?? [];
    public virtual HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public HandledException(string message,
        Dictionary<string, object?>? extraData = default
    ) : this(message, null, extraData) { }
}
