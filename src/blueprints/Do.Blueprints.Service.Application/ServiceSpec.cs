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

    internal static IServiceProvider ServiceProvider => _serviceProvider;
    internal static ISession Session => _serviceProvider.GetRequiredService<ISession>();

    protected static ApplicationContext Init(
        Func<BusinessConfigurator, IFeature<BusinessConfigurator>> business,
        Func<CoreConfigurator, IFeature<CoreConfigurator>>? core = default,
        Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>>? database = default,
        Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>>? exceptionHandling = default,
        Func<MockOverriderConfigurator, IFeature<MockOverriderConfigurator>>? mockOverrider = default,
        Func<OrmConfigurator, IFeature<OrmConfigurator>>? orm = default,
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

        return context;
    }

    ITransaction _transaction = default!;
    internal Dictionary<string, string> Settings { get; private set; } = default!;

    public override void SetUp()
    {
        base.SetUp();

        _transaction = Session.BeginTransaction();

        Settings = new();

        MockMe.TheConfiguration(settings: Settings);
        MockMe.TheSystem(now: new DateTime(2023, 09, 09, 10, 10, 00));
    }

    public override void TearDown()
    {
        base.TearDown();

        Session.Flush();
        _transaction.Rollback();
        Session.Clear();

        GiveMe.The<IMockOverrider>().Reset();
    }
}