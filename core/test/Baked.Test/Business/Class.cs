namespace Baked.Test.Business;

public class Class : Abstract, IInterface
{
    public void Method() { }
    public override void AbstractMethod() { }
    public void InterfaceMethod() { }
    public override int GetHashCode() => base.GetHashCode();
}