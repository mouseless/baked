using Baked.Architecture;
using Baked.Business;
using Baked.Caching;
using Baked.Communication;
using Baked.Core;
using Baked.Database;
using Baked.ExceptionHandling;
using Baked.Localization;
using Baked.MockOverrider;
using Baked.Orm;
using Baked.Testing;
using Baked.Theme;

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
        IEnumerable<Func<CachingConfigurator, IFeature<CachingConfigurator>>>? cachings = default,
        Func<CommunicationConfigurator, IFeature<CommunicationConfigurator>>? communication = default,
        Func<CoreConfigurator, IFeature<CoreConfigurator>>? core = default,
        Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>>? database = default,
        Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>>? exceptionHandling = default,
        Func<LocalizationConfigurator, IFeature<LocalizationConfigurator>>? localization = default,
        Func<MockOverriderConfigurator, IFeature<MockOverriderConfigurator>>? mockOverrider = default,
        Func<OrmConfigurator, IFeature<OrmConfigurator>>? orm = default,
        Func<ThemeConfigurator, IFeature<ThemeConfigurator>>? theme = default,
        Action<ApplicationDescriptor>? configure = default
    )
    {
        cachings ??= [c => c.InMemory(), c => c.ScopedMemory()];
        communication ??= c => c.Mock();
        core ??= c => c.Mock();
        database ??= c => c.InMemory();
        exceptionHandling ??= c => c.ProblemDetails();
        localization ??= c => c.AspNetCore();
        mockOverrider ??= c => c.FirstInterface();
        orm ??= c => c.AutoMap();
        theme ??= c => c.Admin();
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
            app.Features.AddCachings(cachings);
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
            app.Features.AddLocalization(localization);
            app.Features.AddMockOverrider(mockOverrider);
            app.Features.AddOrm(orm);
            app.Features.AddTheme(theme);

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