using Baked.ExceptionHandling;

namespace Baked.Test.ExceptionHandling;

public class TestServiceHandledException(string message)
    : HandledException(message)
{
    public override string LKey => "a_handled_exception_was_thrown";
}