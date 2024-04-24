namespace Do.Authorization;

public class ForbiddenAccessException(string? message, Exception? innerException)
    : Exception(message, innerException)
{
    public ForbiddenAccessException(string? message)
        : this(message, default)
    { }
}
