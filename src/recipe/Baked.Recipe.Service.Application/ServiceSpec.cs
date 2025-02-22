using Baked.Architecture;
using Baked.Business;
using Baked.Caching;
using Baked.Communication;
using Baked.Core;
using Baked.Database;
using Baked.ExceptionHandling;
using Baked.MockOverrider;
using Baked.Orm;
using Baked.Testing;

namespace Baked;

public abstract class ServiceSpec : Spec
{
    public class Enum<T> where T : notnull
    {
        public static IEnumerable<T> Values() =>
            Enum.GetValues(typeof(T)).Cast<int>().Where(it => it > 0).Cast<T>();
    }

    protected static void Init(
        Func<BusinessConfigurator, IFeature<BusinessConfigurator>> business,
        Func<CachingConfigurator, IFeature<CachingConfigurator>>? caching = default,
        Func<CommunicationConfigurator, IFeature<CommunicationConfigurator>>? communication = default,
        Func<CoreConfigurator, IFeature<CoreConfigurator>>? core = default,
        Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>>? database = default,
        Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>>? exceptionHandling = default,
        Func<MockOverriderConfigurator, IFeature<MockOverriderConfigurator>>? mockOverrider = default,
        Func<OrmConfigurator, IFeature<OrmConfigurator>>? orm = default,
        Action<ApplicationDescriptor>? configure = default
    )
    {
        caching ??= c => c.ScopedMemory();
        communication ??= c => c.Mock();
        core ??= c => c.Mock();
        database ??= c => c.InMemory();
        exceptionHandling ??= c => c.ProblemDetails();
        mockOverrider ??= c => c.FirstInterface();
        orm ??= c => c.AutoMap();
        configure ??= _ => { };

        Init(app =>
        {
            app.Layers.AddCodeGeneration();
            app.Layers.AddDataAccess();
            app.Layers.AddDomain();
            app.Layers.AddRuntime();
            app.Layers.AddTesting();

            app.Features.AddBinding(c => c.Rest());
            app.Features.AddBusiness(business);
            app.Features.AddCaching(caching);
            app.Features.AddCodingStyles([
                c => c.AddRemoveChild(),
                c => c.CommandPattern(),
                c => c.EntityExtensionViaComposition(),
                c => c.EntitySubclassViaComposition(),
                c => c.NamespaceAsRoute(),
                c => c.ObjectAsJson(),
                c => c.RecordsAreDtos(),
                c => c.RemainingServicesAreSingleton(),
                c => c.RichEntity(),
                c => c.RichTransient(),
                c => c.ScopedBySuffix(),
                c => c.SingleByUnique(),
                c => c.UriReturnIsRedirect(),
                c => c.UseBuiltInTypes(),
                c => c.UseNullableTypes(),
                c => c.WithMethod()
            ]);
            app.Features.AddCommunication(communication);
            app.Features.AddCore(core);
            app.Features.AddDatabase(database);
            app.Features.AddExceptionHandling(exceptionHandling);
            app.Features.AddLifetimes([
                c => c.Scoped(),
                c => c.Singleton(),
                c => c.Transient()
            ]);
            app.Features.AddMockOverrider(mockOverrider);
            app.Features.AddOrm(orm);

            configure(app);
        });
    }

    public override void SetUp()
    {
        base.SetUp();

        // overrides configuration mock in `MockCoreFeature` with below default value provider
        MockMe.TheConfiguration(defaultValueProvider: GetDefaultSettingsValue);
    }

    protected virtual string? GetDefaultSettingsValue(string key) =>
        "test value";
}