namespace Do.Test.ExceptionHandling;

public class ExceptionSamples
{
    public void Throw(bool handled)
    {
        if (handled)
        {
            throw new TestServiceHandledException("A handled exception was thrown");
        }

        throw new InvalidOperationException();
    }
}
