using Do.Business;

namespace Do.Test;

[Singleton]
public class Class
{
    public int Id { get; set; }

    [AuthorizationRequired]
    public void VoidMethod() { }

    internal Internal InternalMethod() =>
        new();
}
