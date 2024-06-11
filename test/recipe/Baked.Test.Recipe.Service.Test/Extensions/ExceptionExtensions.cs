namespace Baked.Test;

public static class ExceptionExtensions
{
    public static void ShouldThrowExceptionWithServiceNotRegisteredMessage<T>(this Func<object> source) =>
        source.ShouldThrowExceptionWithServiceNotRegisteredMessage(typeof(T));

    public static void ShouldThrowExceptionWithServiceNotRegisteredMessage(this Func<object> source, Type serviceType) =>
        source.ShouldThrow<Exception>().Message.ShouldBe($"No service for type '{serviceType}' has been registered.");
}