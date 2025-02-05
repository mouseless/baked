using Baked.ExceptionHandling;

namespace Baked.Test.ExceptionHandling;

public class TestServiceHandledException(string message)
    : HandledException(message);