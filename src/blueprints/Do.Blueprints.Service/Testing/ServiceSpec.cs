using Do.Architecture;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;

namespace Do.Testing;

// WARNING: this belongs to Do.Blueprints.Service, do NOT move this into any
// extension
public abstract class ServiceSpec : Spec
{
    static IServiceProvider _serviceProvider = default!;
    static ISession _session = default!;

    static IServiceProvider ServiceProvider => _serviceProvider;

    static ApplicationContext Init()
    {
        var context = Spec.Init(app =>
        {
            app.Layers.AddConfiguration();
            app.Layers.AddDataAccess();
            app.Layers.AddDependencyInjection();
            app.Layers.AddMonitoring();
            app.Layers.AddRestApi();
            app.Layers.AddTesting();

            app.Features.AddCore(c => c.Mock());
        });

        var services = context.Get<IServiceCollection>();

        _serviceProvider = services.BuildServiceProvider();
        _serviceProvider.CreateScope();
        _session = _serviceProvider.GetRequiredService<ISession>();

        return context;
    }

    ITransaction _transaction = default!;

    public override void SetUp()
    {
        base.SetUp();

        _transaction = _session.BeginTransaction();
    }

    public override void TearDown()
    {
        base.TearDown();

        _session.Flush();
        _transaction.Rollback();
        _session.Clear();
    }
}
