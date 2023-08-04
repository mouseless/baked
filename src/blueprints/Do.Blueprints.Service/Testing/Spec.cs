using Do.Architecture;
using Do.Branding;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NHibernate;
using NUnit.Framework;

namespace Do.Testing;

public abstract class Spec
{
    static readonly IServiceProvider _serviceProvider;
    static readonly IServiceScope _globalScope;
    static readonly ISession _session;

    static IServiceProvider ServiceProvider => _serviceProvider;

    static Spec()
    {
        var context = new ApplicationContext();

        new Forge(new Mock<IBanner>().Object, () => new Application(context))
            .Application(app =>
            {
                app.Layers.AddConfiguration();
                app.Layers.AddDataAccess();
                app.Layers.AddDependencyInjection();
                app.Layers.AddMonitoring();
                app.Layers.AddRestApi();
                app.Layers.AddTesting();

                app.Features.AddCore(c => c.Mock());
            })
            .Run();

        var services = context.Get<IServiceCollection>();

        _serviceProvider = services.BuildServiceProvider();
        _globalScope = _serviceProvider.CreateScope();
        _session = _serviceProvider.GetRequiredService<ISession>();
    }

    ITransaction _transaction = default!;

    public Stubber GiveMe { get; private set; } = default!;
    public Mocker MockMe { get; private set; } = default!;

    [SetUp]
    public virtual void SetUp()
    {
        GiveMe = new(this);
        MockMe = new(this);

        _transaction = _session.BeginTransaction();
    }

    [TearDown]
    public virtual void TearDown()
    {
        _session.Flush();
        _transaction.Rollback();
        _session.Clear();
    }
}
