using Do.Business;

namespace Do.Test;

[Singleton]
public class Class
{
    [AuthorizationRequired]
    public void VoidMethod() { }

    internal Internal InternalMethod() =>
        new();
}
