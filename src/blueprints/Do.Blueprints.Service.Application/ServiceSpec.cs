using Do.Architecture;
using Do.Business;
using Do.Core;
using Do.Database;
using Do.ExceptionHandling;
using Do.MockOverrider;
using Do.Orm;
using Do.Testing;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;

using ITransaction = NHibernate.ITransaction;

namespace Do;

public abstract class ServiceSpec : Spec
{
    static IServiceProvider _serviceProvider = default!;
    static ISession _session = default!;

    internal static IServiceProvider ServiceProvider => _serviceProvider;

    protected static ApplicationContext Init(
        Func<BusinessConfigurator, IBusinessFeature> business,
        Func<CoreConfigurator, ICoreFeature>? core = default,
        Func<DatabaseConfigurator, IDatabaseFeature>? database = default,
        Func<ExceptionHandlingConfigurator, IExceptionHandlingFeature>? exceptionHandling = default,
        Func<MockOverriderConfigurator, IMockOverriderFeature>? mockOverrider = default,
        Func<OrmConfigurator, IOrmFeature>? orm = default,
        Action<ApplicationDescriptor>? configure = default
    )
    {
        core ??= c => c.Mock();
        database ??= c => c.InMemory();
        exceptionHandling ??= c => c.Default();
        mockOverrider ??= c => c.FirstInterface();
        orm ??= c => c.Default();

        var context = Spec.Init(app =>
        {
            app.Layers.AddConfiguration();
            app.Layers.AddDataAccess();
            app.Layers.AddDependencyInjection();
            app.Layers.AddMonitoring();
            app.Layers.AddRestApi();
            app.Layers.AddTesting();

            app.Features.AddBusiness(business);
            app.Features.AddCore(core);
            app.Features.AddDatabase(database);
            app.Features.AddExceptionHandling(exceptionHandling);
            app.Features.AddMockOverrider(mockOverrider);
            app.Features.AddOrm(orm);

            configure?.Invoke(app);
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
