using Do.Architecture;
using NUnit.Framework;

namespace Do.Testing;

public abstract class Nfr
{
    protected static void Init(string[] args) =>
        new Runner(args).Run();

    protected virtual string EnvironmentName => "Integration";

    protected abstract Application ForgeApplication();

    [OneTimeSetUp]
    public virtual void OneTimeSetUp() { }

    [OneTimeTearDown]
    public virtual void OneTimeTearDown() { }

    [SetUp]
    public virtual void SetUp() { }

    [TearDown]
    public virtual void TearDown() { }

    protected class Runner(string[] _args)
    {
        Nfr Instance => (Nfr)(Activator.CreateInstance(Type) ?? throw new($"Could not create instance of {Type}"));
        Type Type => Type.GetType(TypeName) ?? throw new($"Type with name '{TypeName}' could not be found ");
        string TypeName => _args[0][(_args[0].IndexOf('=') + 1)..];

        public void Run() => Instance.ForgeApplication().Run();
    }
}
