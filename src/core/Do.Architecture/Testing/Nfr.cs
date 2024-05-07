using Do.Architecture;
using NUnit.Framework;

namespace Do.Testing;

public abstract class Nfr
{
    protected static void Init(string[] args)
    {
        var typeName = args[0][(args[0].IndexOf('=') + 1)..];
        var type = Type.GetType(typeName) ?? throw new($"Type with name '{typeName}' could not be found ");
        var instance = (Nfr)(Activator.CreateInstance(type) ?? throw new($"Could not create instance of {type}"));

        instance.ForgeApplication().Run();
    }

    protected virtual string EnvironmentName => "Nfr";

    protected abstract Application ForgeApplication();

    [OneTimeSetUp]
    public virtual Task OneTimeSetUp() => Task.CompletedTask;

    [OneTimeTearDown]
    public virtual Task OneTimeTearDown() => Task.CompletedTask;

    [SetUp]
    public virtual void SetUp() { }

    [TearDown]
    public virtual void TearDown() { }
}