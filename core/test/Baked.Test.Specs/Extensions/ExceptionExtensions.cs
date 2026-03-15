using Baked.Testing;

namespace Baked.Test;

public static class ExceptionExtensions
{
    extension(Stubber _)
    {
        public Exception AnException() =>
            new("TEST EXCEPTION");
    }

    extension(Func<object> func)
    {
        public void ShouldThrowExceptionWithServiceNotRegisteredMessage<T>() =>
            func.ShouldThrowExceptionWithServiceNotRegisteredMessage(typeof(T));

        public void ShouldThrowExceptionWithServiceNotRegisteredMessage(Type serviceType) =>
            func.ShouldThrow<Exception>().Message.ShouldBe($"No service for type '{serviceType}' has been registered.");
    }
}