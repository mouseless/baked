using Do.ExceptionHandling;

namespace Do.Test;

public class TestServiceHandledException(string _message)
    : HandledException(_message) { }
