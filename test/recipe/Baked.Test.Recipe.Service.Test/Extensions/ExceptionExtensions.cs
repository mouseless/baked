namespace Baked.Test;

public static class ExceptionExtensions
{
    public static void ShouldThrowExceptionWithServiceNotRegisteredMessage<T>(this Func<object> func) =>
        func.ShouldThrowExceptionWithServiceNotRegisteredMessage(typeof(T));

    public static void ShouldThrowExceptionWithServiceNotRegisteredMessage(this Func<object> func, Type serviceType) =>
        func.ShouldThrow<Exception>().Message.ShouldBe($"No service for type '{serviceType}' has been registered.");
}