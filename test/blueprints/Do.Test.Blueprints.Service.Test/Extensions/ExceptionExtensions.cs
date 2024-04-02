namespace Do.Test;

public static class ExceptionExtensions
{
    public static void ShouldThrowExceptionWithServiceNotRegisteredMessage(this Func<object> source, Type serviceType) =>
        source.ShouldThrow<Exception>().Message.ShouldBe($"No service for type '{serviceType}' has been registered.");
}
