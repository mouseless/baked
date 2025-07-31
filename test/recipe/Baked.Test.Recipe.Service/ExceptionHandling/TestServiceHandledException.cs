using Baked.ExceptionHandling;

namespace Baked.Test.ExceptionHandling;

public class TestServiceHandledException(string? message = default)
    : HandledException(message ?? "A handled exception was thrown");