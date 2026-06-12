namespace Baked.Test.Buildtime;

public class TestFeature
{
    public void Configure(Action action) =>
        action();
}