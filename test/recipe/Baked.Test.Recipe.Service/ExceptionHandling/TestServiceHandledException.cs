using Baked.ExceptionHandling;

namespace Baked.Test.ExceptionHandling;

public class TestServiceHandledException(string? message = default)
    : HandledException(message ?? "A_handled_exception_was_thrown");