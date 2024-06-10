using Baked.Architecture;
using Baked.Branding;
using Moq;
using NUnit.Framework;

namespace Baked.Testing;

public abstract class Spec
{
    protected static ApplicationContext Init(
        Action<ApplicationDescriptor>? describe = default
    )
    {
        var result = new ApplicationContext();

        new Bake(new Mock<IBanner>().Object, () => new(result))
            .Application(describe ?? (_ => { }))
            .Run();

        return result;
    }

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
    }

    [TearDown]
    public virtual void TearDown() { }
}