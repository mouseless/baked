using Baked.Authorization;

namespace Baked.Playground.ExceptionHandling;

[AllowAnonymous]
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

    public void GetHandled() =>
        throw new TestServiceHandledException();
}