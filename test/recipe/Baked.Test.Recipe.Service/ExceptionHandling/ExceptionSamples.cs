namespace Baked.Test.ExceptionHandling;

public class ExceptionSamples
{
    public void Throw(bool handled)
    {
        if (handled)
        {
            throw new TestServiceHandledException();
        }

        throw new InvalidOperationException();
    }
}