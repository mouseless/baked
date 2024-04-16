using Do.ExceptionHandling;

namespace Do.Test.ExceptionHandling;

public class TestServiceHandledException(string message)
    : HandledException(message)
{ }