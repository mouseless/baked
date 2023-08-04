using Do.Architecture;
using Do.Business;
using Do.Core;
using Do.MockOverrider;
using Do.Testing;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;

namespace Do;

public abstract class ServiceSpec : Spec
{
    static IServiceProvider _serviceProvider = default!;
    static ISession _session = default!;

    internal static IServiceProvider ServiceProvider => _serviceProvider;

    protected static ApplicationContext Init(
        Func<BusinessConfigurator, IFeature> business,
        Func<CoreConfigurator, IFeature>? core = default,
        Func<MockOverriderConfigurator, IFeature>? mockOverrider = default,
        Action<ApplicationDescriptor>? configure = default
    )
    {
        core ??= c => c.Mock();
        mockOverrider ??= c => c.FirstInterface();

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
            app.Features.AddMockOverrider(mockOverrider);

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
