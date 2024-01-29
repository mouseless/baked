namespace Do.Test;

public class Class
{
    [AuthorizationRequired]
    public void VoidMethod() { }

    internal Internal InternalMethod() =>
        new();
}
