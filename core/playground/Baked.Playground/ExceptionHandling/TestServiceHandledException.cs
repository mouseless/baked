using Baked.ExceptionHandling;

namespace Baked.Playground.ExceptionHandling;

public class TestServiceHandledException(string? message = default)
    : HandledException(message ?? "A handled exception was thrown");