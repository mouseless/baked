using Do.Architecture;
using Do.Testing;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;

namespace Do;

public abstract class ServiceSpec : Spec
{
    static IServiceProvider _serviceProvider = default!;
    static ISession _session = default!;

    internal static IServiceProvider ServiceProvider => _serviceProvider;

    protected new static ApplicationContext Init(
        Action<ApplicationDescriptor>? describe = default
    )
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
            app.Features.AddMockOverrider(c => c.FirstInterface());

            describe?.Invoke(app);
        });

        _serviceProvider = context.GetServiceProvider();
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
