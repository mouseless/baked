using NUnit.Framework;
using System.Reflection;

namespace Baked.Testing;

public abstract class Nfr
{
    public static Assembly? EntryAssembly { get; private set; }

    protected static void Init<TEntryPoint>() where TEntryPoint : class
    {
        EntryAssembly = typeof(TEntryPoint).Assembly;
    }

    [OneTimeSetUp]
    public virtual Task OneTimeSetUp() => Task.CompletedTask;

    [OneTimeTearDown]
    public virtual Task OneTimeTearDown() => Task.CompletedTask;

    [SetUp]
    public virtual void SetUp() { }

    [TearDown]
    public virtual void TearDown() { }
}