using Do.Architecture;
using Do.Business;
using Do.Core;
using Do.Database;
using Do.ExceptionHandling;
using Do.MockOverrider;
using Do.Orm;
using Do.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NHibernate;

using ITransaction = NHibernate.ITransaction;

namespace Do;

public abstract class ServiceSpec : Spec
{
    static IServiceProvider _serviceProvider = default!;
    static ISession _session = default!;

    internal static IServiceProvider ServiceProvider => _serviceProvider;

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
        _session = _serviceProvider.GetRequiredService<ISession>();

        return context;
    }

    ITransaction _transaction = default!;
    internal Dictionary<string, string> Settings { get; private set; } = default!;
    internal DateTime SystemNow { get; private set; } = default!;

    public override void SetUp()
    {
        base.SetUp();

        _transaction = _session.BeginTransaction();

        Mock.Get(GiveMe.The<IConfiguration>())
           .Setup(c => c.GetSection(It.IsAny<string>())).Returns((string key) =>
           {
               var mockSection = new Mock<IConfigurationSection>();

               mockSection.Setup(s => s.Value).Returns(() =>
               {
                   if (Settings.TryGetValue(key, out var result))
                   {
                       return result;
                   }

                   return key.EndsWith("Url") ? "https://test.com?value" : "test value";
               });

               return mockSection.Object;
           });

        Mock.Get(GiveMe.The<ISystem>())
           .Setup(c => c.Now).Returns(SystemNow);
    }

    public override void TearDown()
    {
        base.TearDown();

        _session.Flush();
        _transaction.Rollback();
        _session.Clear();

        GiveMe.The<IMockOverrider>().Reset();
    }
}