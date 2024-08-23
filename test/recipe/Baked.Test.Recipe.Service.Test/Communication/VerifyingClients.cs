namespace Baked.Test.Communication;

public class VerifyingClients : TestServiceSpec
{
    [Test]
    public void Verify_includes_parameters_when_given() => this.ShouldFail();

    [Test]
    public void Verify_parameters_are_optional() => this.ShouldFail();
}
