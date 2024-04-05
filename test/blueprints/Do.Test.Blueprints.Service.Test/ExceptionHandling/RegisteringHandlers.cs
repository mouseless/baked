namespace Do.Test.ExceptionHandling;

public class RegisteringHandlers : TestServiceSpec
{
    [Test]
    public void Exception_handlers_are_registered_as_singleton_and_forwarded_to_IExceptionHandler_interface()
    {
        var exceptionHandlers = GiveMe.The<IEnumerable<Do.ExceptionHandling.IExceptionHandler>>();
        var expected = GiveMe.The<SampleExceptionHandler>();

        var actual = exceptionHandlers.FirstOrDefault(h => h is SampleExceptionHandler);

        actual.ShouldBeSameAs(expected);
    }
}