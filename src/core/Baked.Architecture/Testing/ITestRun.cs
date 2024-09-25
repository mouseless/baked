namespace Baked.Testing;

public interface ITestRun
{
    void SetUp(Spec spec);
    void TearDown(Spec spec);
}