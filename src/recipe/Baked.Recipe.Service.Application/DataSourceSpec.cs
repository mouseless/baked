using Baked.Architecture;
using Baked.Business;
using Baked.Caching;
using Baked.Core;
using Baked.Database;
using Baked.ExceptionHandling;
using Baked.MockOverrider;
using Baked.Reporting;
using Baked.Testing;

namespace Baked;

public abstract class DataSourceSpec : Spec
{
    protected static void Init(
        Func<BusinessConfigurator, IFeature<BusinessConfigurator>> business,
        Func<CachingConfigurator, IFeature<CachingConfigurator>>? caching = default,
        Func<CoreConfigurator, IFeature<CoreConfigurator>>? core = default,
        Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>>? database = default,
        Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>>? exceptionHandling = default,
        Func<MockOverriderConfigurator, IFeature<MockOverriderConfigurator>>? mockOverrider = default,
        Func<ReportingConfigurator, IFeature<ReportingConfigurator>>? reporting = default,
        Action<ApplicationDescriptor>? configure = default
    )
    {
        caching ??= c => c.ScopedMemory();
        core ??= c => c.Mock();
        database ??= c => c.InMemory();
        exceptionHandling ??= c => c.ProblemDetails();
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

            app.Features.AddBusiness(business);
            app.Features.AddCaching(caching);
            app.Features.AddCodingStyles([
                c => c.AddRemoveChild(),
                c => c.CommandPattern(),
                c => c.NamespaceAsRoute(),
                c => c.RecordsAreDtos(),
                c => c.RemainingServicesAreSingleton(),
                c => c.ScopedBySuffix(),
                c => c.UseBuiltInTypes(),
                c => c.UseNullableTypes(),
                c => c.WithMethod()
            ]);
            app.Features.AddCore(core);
            app.Features.AddDatabase(database);
            app.Features.AddExceptionHandling(exceptionHandling);
            app.Features.AddLifetimes([
                c => c.Scoped(),
                c => c.Singleton(),
                c => c.Transient()
            ]);
            app.Features.AddMockOverrider(mockOverrider);
            app.Features.AddReporting(reporting);

            configure(app);
        });
    }
}