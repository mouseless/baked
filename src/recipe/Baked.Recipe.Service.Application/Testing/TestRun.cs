namespace Baked.Testing;

public class TestRun(TestConfiguration _configuration)
    : ITestRun
{
    public void SetUp(Spec spec)
    {
        foreach (var setUp in _configuration.SetUps)
        {
            setUp(spec);
        }
    }

    public void TearDown(Spec spec)
    {
        foreach (var tearDown in _configuration.TearDowns)
        {
            tearDown(spec);
        }
    }
}