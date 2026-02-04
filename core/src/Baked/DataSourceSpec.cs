using Baked.Architecture;
using Baked.Business;
using Baked.Caching;
using Baked.Core;
using Baked.Database;
using Baked.ExceptionHandling;
using Baked.Localization;
using Baked.MockOverrider;
using Baked.Reporting;
using Baked.Testing;

namespace Baked;

public abstract class DataSourceSpec : Spec
{
    protected static void Init(
        Func<BusinessConfigurator, IFeature<BusinessConfigurator>> business,
        IEnumerable<Func<CachingConfigurator, IFeature<CachingConfigurator>>>? cachings = default,
        Func<CoreConfigurator, IFeature<CoreConfigurator>>? core = default,
        Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>>? database = default,
        Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>>? exceptionHandling = default,
        Func<LocalizationConfigurator, IFeature<LocalizationConfigurator>>? localization = default,
        Func<MockOverriderConfigurator, IFeature<MockOverriderConfigurator>>? mockOverrider = default,
        Func<ReportingConfigurator, IFeature<ReportingConfigurator>>? reporting = default,
        Action<ApplicationDescriptor>? configure = default
    )
    {
        cachings ??= [c => c.InMemory(), c => c.ScopedMemory()];
        core ??= c => c.Mock();
        database ??= c => c.InMemory();
        exceptionHandling ??= c => c.ProblemDetails();
        localization ??= c => c.Dotnet();
        mockOverrider ??= c => c.FirstInterface();
        reporting ??= c => c.Mock();
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
                c => c.Id(),
                c => c.Initializable(),
                c => c.Locatable(),
                c => c.NamespaceAsRoute(),
                c => c.RecordsAreDtos(),
                c => c.RemainingServicesAreSingleton(),
                c => c.RichTransient(),
                c => c.ScopedBySuffix(),
                c => c.UseBuiltInTypes(),
                c => c.UseNullableTypes()
            ]);
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
            app.Features.AddReporting(reporting);

            configure(app);
        });
    }
}