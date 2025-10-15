using Baked.Architecture;
using Baked.Branding;
using Moq;
using NUnit.Framework;

namespace Baked.Testing;

public abstract class Spec
{
    private static ApplicationContext _startContext = new();
    private static ApplicationContext _generateContext = new();

    public static ApplicationContext StartContextStatic => _startContext;
    public static ApplicationContext GenerateContextStatic => _generateContext;

    public ApplicationContext StartContext => _startContext;
    public ApplicationContext GenerateContext => _generateContext;

    protected static void Init(Action<ApplicationDescriptor> describe) =>
        new Bake(new Mock<IBanner>().Object, () => new(_startContext, _generateContext: _generateContext), _runFlags: RunFlags.Generate | RunFlags.Start)
            .Application(describe)
            .Run();

    public Stubber GiveMe { get; private set; } = default!;
    public Mocker MockMe { get; private set; } = default!;

    [OneTimeSetUp]
    public virtual void OneTimeSetUp() { }

    [OneTimeTearDown]
    public virtual void OneTimeTearDown() { }

    [SetUp]
    public virtual void SetUp()
    {
        GiveMe = new(this);
        MockMe = new(this);

        if (_startContext.Has<ITestRun>())
        {
            _startContext.Get<ITestRun>().SetUp(this);
        }
    }

    [TearDown]
    public virtual void TearDown()
    {
        if (_startContext.Has<ITestRun>())
        {
            _startContext.Get<ITestRun>().TearDown(this);
        }
    }
}