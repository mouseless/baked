using Baked.Architecture;
using Baked.Business;
using Baked.Caching;
using Baked.CodingStyle;
using Baked.CodingStyle.CommandPattern;
using Baked.CodingStyle.Initializable;
using Baked.CodingStyle.Label;
using Baked.CodingStyle.ScopedBySuffix;
using Baked.CodingStyle.UseBuiltInTypes;
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

        Func<CodingStyleConfigurator, CommandPatternCodingStyleFeature>? commandPattern = default,
        Func<CodingStyleConfigurator, InitializableCodingStyleFeature>? initializable = default,
        Func<CodingStyleConfigurator, LabelCodingStyleFeature>? label = default,
        Func<CodingStyleConfigurator, ScopedBySuffixCodingStyleFeature>? scopedBySuffix = default,
        Func<CodingStyleConfigurator, UseBuiltInTypesCodingStyleFeature>? useBuiltInTypes = default,

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

        commandPattern ??= c => c.CommandPattern();
        initializable ??= c => c.Initializable();
        label ??= c => c.Label();
        scopedBySuffix ??= c => c.ScopedBySuffix();
        useBuiltInTypes ??= c => c.UseBuiltInTypes();

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
                commandPattern,
                c => c.Id(),
                initializable,
                label,
                c => c.Locatable(),
                c => c.NamespaceAsRoute(),
                c => c.Query(),
                c => c.RecordsAreDtos(),
                c => c.RemainingServicesAreSingleton(),
                c => c.RichTransient(),
                scopedBySuffix,
                useBuiltInTypes,
                c => c.UseNullableTypes(),
                c => c.ValueType()
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