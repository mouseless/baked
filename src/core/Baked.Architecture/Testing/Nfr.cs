using NUnit.Framework;

namespace Do.Testing;

public abstract class Nfr
{
    [OneTimeSetUp]
    public virtual Task OneTimeSetUp() => Task.CompletedTask;

    [OneTimeTearDown]
    public virtual Task OneTimeTearDown() => Task.CompletedTask;

    [SetUp]
    public virtual void SetUp() { }

    [TearDown]
    public virtual void TearDown() { }
}