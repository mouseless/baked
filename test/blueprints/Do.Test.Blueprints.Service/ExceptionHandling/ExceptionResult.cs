namespace Do.Test;

public class ExceptionResult
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
