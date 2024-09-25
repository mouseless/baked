using Baked.Architecture;
using Baked.Branding;
using Moq;
using NUnit.Framework;

namespace Baked.Testing;

public abstract class Spec
{
    private static ApplicationContext _context = new();

    public static ApplicationContext ContextStatic => _context;
    public ApplicationContext Context => _context;

    protected static void Init(Action<ApplicationDescriptor> describe) =>
        new Bake(new Mock<IBanner>().Object, () => new(_context))
            .Application(describe)
            .Run();

    public Stubber GiveMe { get; private set; } = default!;
    public Mocker MockMe { get; private set; } = default!;
    public Searcher GetMe { get; private set; } = default!;

    [OneTimeSetUp]
    public virtual void OneTimeSetUp() { }

    [OneTimeTearDown]
    public virtual void OneTimeTearDown() { }

    [SetUp]
    public virtual void SetUp()
    {
        GiveMe = new(this);
        MockMe = new(this);
        GetMe = new(this);

        if (_context.Has<ITestRun>())
        {
            _context.Get<ITestRun>().SetUp(this);
        }
    }

    [TearDown]
    public virtual void TearDown()
    {
        if (_context.Has<ITestRun>())
        {
            _context.Get<ITestRun>().TearDown(this);
        }
    }
}