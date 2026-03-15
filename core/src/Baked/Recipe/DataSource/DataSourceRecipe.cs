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
using Baked.Greeting;
using Baked.Localization;
using Baked.Logging;
using Baked.MockOverrider;
using Baked.RateLimiter;
using Baked.Reporting;

namespace Baked.Recipe.DataSource;

public class DataSourceRecipe(Func<BusinessConfigurator, IFeature<BusinessConfigurator>> _business, ExecutionMode mode = ExecutionMode.Run)
{
    public ExecutionMode Mode { get; } = mode;
    public Func<BusinessConfigurator, IFeature<BusinessConfigurator>> Business { get; set; } = _business;
    public IEnumerable<Func<CachingConfigurator, IFeature<CachingConfigurator>>> Cachings { get; set; } = [c => c.InMemory(), c => c.ScopedMemory()];
    public Func<CoreConfigurator, IFeature<CoreConfigurator>> Core { get; set; } = mode == ExecutionMode.Run ? c => c.Dotnet() : c => c.Mock();
    public Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>> Database { get; set; } = mode == ExecutionMode.Run ? c => c.Sqlite() : c => c.InMemory();
    public Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>> ExceptionHandling { get; set; } = c => c.ProblemDetails();
    public Func<GreetingConfigurator, IFeature<GreetingConfigurator>>? Greeting { get; set; } = mode == ExecutionMode.Run ? c => c.Swagger() : default;
    public Func<LocalizationConfigurator, IFeature<LocalizationConfigurator>> Localization { get; set; } = c => c.Dotnet();
    public Func<LoggingConfigurator, IFeature<LoggingConfigurator>>? Logging { get; set; } = mode == ExecutionMode.Run ? c => c.Request() : default;
    public Func<MockOverriderConfigurator, IFeature<MockOverriderConfigurator>>? MockOverrider { get; set; } = mode == ExecutionMode.Test ? c => c.FirstInterface() : default;
    public Func<RateLimiterConfigurator, IFeature<RateLimiterConfigurator>>? RateLimiter { get; set; } = mode == ExecutionMode.Run ? c => c.Concurrency() : default;
    public Func<ReportingConfigurator, IFeature<ReportingConfigurator>> Reporting { get; set; } = mode == ExecutionMode.Run ? c => c.NativeSql() : c => c.Mock();

    public Func<CodingStyleConfigurator, CommandPatternCodingStyleFeature> CommandPattern { get; set; } = c => c.CommandPattern();
    public Func<CodingStyleConfigurator, InitializableCodingStyleFeature> Initializable { get; set; } = c => c.Initializable();
    public Func<CodingStyleConfigurator, LabelCodingStyleFeature> Label { get; set; } = c => c.Label();
    public Func<CodingStyleConfigurator, ScopedBySuffixCodingStyleFeature> ScopedBySuffix { get; set; } = c => c.ScopedBySuffix();
    public Func<CodingStyleConfigurator, UseBuiltInTypesCodingStyleFeature> UseBuiltInTypes { get; set; } = c => c.UseBuiltInTypes();

    public Action<ApplicationDescriptor> Configure { get; set; } = _ => { };

    public void Apply(ApplicationDescriptor app)
    {
        app.Layers.AddCodeGeneration();
        app.Layers.AddDataAccess();
        app.Layers.AddDomain();
        app.Layers.AddRuntime();
        if (Mode == ExecutionMode.Test) { app.Layers.AddTesting(); }
        if (Mode == ExecutionMode.Run) { app.Layers.AddHttpServer(); }
        if (Mode == ExecutionMode.Run) { app.Layers.AddRestApi(); }

        app.Features.AddBinding(c => c.Rest());
        app.Features.AddBusiness(Business);
        app.Features.AddCachings(Cachings);
        app.Features.AddCodingStyles(
        [
            c => c.AddRemoveChild(),
            CommandPattern,
            c => c.Id(),
            Initializable,
            Label,
            c => c.Locatable(),
            c => c.NamespaceAsRoute(),
            c => c.Query(),
            c => c.RecordsAreDtos(),
            c => c.RemainingServicesAreSingleton(),
            c => c.RichTransient(),
            ScopedBySuffix,
            UseBuiltInTypes,
            c => c.UseNullableTypes(),
            c => c.ValueType()
        ]);

        app.Features.AddCore(Core);
        app.Features.AddDatabase(Database);
        app.Features.AddExceptionHandling(ExceptionHandling);
        if (Greeting is not null) { app.Features.AddGreeting(Greeting); }
        app.Features.AddLifetimes(
        [
            c => c.Scoped(),
            c => c.Singleton(),
            c => c.Transient()
        ]);
        app.Features.AddLocalization(Localization);
        if (Logging is not null) { app.Features.AddLogging(Logging); }
        app.Features.AddReporting(Reporting);
        if (RateLimiter is not null) { app.Features.AddRateLimiter(RateLimiter); }

        if (MockOverrider is not null) { app.Features.AddMockOverrider(MockOverrider); }

        Configure(app);
    }
}