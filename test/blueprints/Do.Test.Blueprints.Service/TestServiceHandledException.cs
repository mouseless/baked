using Do.ExceptionHandling;

namespace Do.Test;

public class TestServiceHandledException : HandledException
{
    public TestServiceHandledException(string message) : base(message) { }
}
