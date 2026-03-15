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

public abstract class DataSourceRecipe(Func<BusinessConfigurator, IFeature<BusinessConfigurator>> _business, ExecutionMode _mode)
{
    public class Test(Func<BusinessConfigurator, IFeature<BusinessConfigurator>> _business) : DataSourceRecipe(_business, ExecutionMode.Test);
    public class Run : DataSourceRecipe
    {
        public Run(Func<BusinessConfigurator, IFeature<BusinessConfigurator>> business) : base(business, ExecutionMode.Run)
        {
            Core(c => c.Dotnet());
            Database(c => c.Sqlite());
            Greeting(c => c.Swagger());
            Logging(c => c.Request());
            MockOverrider(default);
            RateLimiter(c => c.Concurrency());
            Reporting(c => c.NativeSql());
        }
    }

    Func<BusinessConfigurator, IFeature<BusinessConfigurator>> _business = _business;
    IEnumerable<Func<CachingConfigurator, IFeature<CachingConfigurator>>> _cachings = [c => c.InMemory(), c => c.ScopedMemory()];
    Func<CoreConfigurator, IFeature<CoreConfigurator>> _core = c => c.Mock();
    Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>> _database = c => c.InMemory();
    Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>> _exceptionHandling = c => c.ProblemDetails();
    Func<GreetingConfigurator, IFeature<GreetingConfigurator>>? _greeting = default;
    Func<LocalizationConfigurator, IFeature<LocalizationConfigurator>> _localization = c => c.Dotnet();
    Func<LoggingConfigurator, IFeature<LoggingConfigurator>>? _logging = default;
    Func<MockOverriderConfigurator, IFeature<MockOverriderConfigurator>>? _mockOverrider = c => c.FirstInterface();
    Func<RateLimiterConfigurator, IFeature<RateLimiterConfigurator>>? _rateLimiter = default;
    Func<ReportingConfigurator, IFeature<ReportingConfigurator>> _reporting = c => c.Mock();

    Func<CodingStyleConfigurator, CommandPatternCodingStyleFeature> _commandPattern = c => c.CommandPattern();
    Func<CodingStyleConfigurator, InitializableCodingStyleFeature> _initializable = c => c.Initializable();
    Func<CodingStyleConfigurator, LabelCodingStyleFeature> _label = c => c.Label();
    Func<CodingStyleConfigurator, ScopedBySuffixCodingStyleFeature> _scopedBySuffix = c => c.ScopedBySuffix();
    Func<CodingStyleConfigurator, UseBuiltInTypesCodingStyleFeature> _useBuiltInTypes = c => c.UseBuiltInTypes();

    Action<ApplicationDescriptor> _configure = _ => { };

    public void Cachings(params Func<CachingConfigurator, IFeature<CachingConfigurator>>[] cachings) =>
        _cachings = cachings;

    public void Core(Func<CoreConfigurator, IFeature<CoreConfigurator>> core) =>
        _core = core;

    public void Database(Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>> database) =>
        _database = database;

    public void ExceptionHandling(Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>> exceptionHandling) =>
        _exceptionHandling = exceptionHandling;

    public void Greeting(Func<GreetingConfigurator, IFeature<GreetingConfigurator>>? greeting) =>
        _greeting = greeting;

    public void Localization(Func<LocalizationConfigurator, IFeature<LocalizationConfigurator>> localization) =>
        _localization = localization;

    public void Logging(Func<LoggingConfigurator, IFeature<LoggingConfigurator>>? logging) =>
        _logging = logging;

    public void MockOverrider(Func<MockOverriderConfigurator, IFeature<MockOverriderConfigurator>>? mockOverrider) =>
        _mockOverrider = mockOverrider;

    public void RateLimiter(Func<RateLimiterConfigurator, IFeature<RateLimiterConfigurator>>? rateLimiter) =>
        _rateLimiter = rateLimiter;

    public void Reporting(Func<ReportingConfigurator, IFeature<ReportingConfigurator>> reporting) =>
        _reporting = reporting;

    public void CommandPattern(Func<CodingStyleConfigurator, CommandPatternCodingStyleFeature> commandPattern) =>
        _commandPattern = commandPattern;

    public void Initializable(Func<CodingStyleConfigurator, InitializableCodingStyleFeature> initializable) =>
        _initializable = initializable;

    public void Label(Func<CodingStyleConfigurator, LabelCodingStyleFeature> label) =>
        _label = label;

    public void ScopedBySuffix(Func<CodingStyleConfigurator, ScopedBySuffixCodingStyleFeature> scopedBySuffix) =>
        _scopedBySuffix = scopedBySuffix;

    public void UseBuiltInTypes(Func<CodingStyleConfigurator, UseBuiltInTypesCodingStyleFeature> useBuiltInTypes) =>
        _useBuiltInTypes = useBuiltInTypes;

    public void Configure(Action<ApplicationDescriptor> configure) =>
        _configure += configure;

    public void Apply(ApplicationDescriptor app)
    {
        app.Layers.AddCodeGeneration();
        app.Layers.AddDataAccess();
        app.Layers.AddDomain();
        app.Layers.AddRuntime();
        if (_mode == ExecutionMode.Test) { app.Layers.AddTesting(); }
        if (_mode == ExecutionMode.Run) { app.Layers.AddHttpServer(); }
        if (_mode == ExecutionMode.Run) { app.Layers.AddRestApi(); }

        app.Features.AddBinding(c => c.Rest());
        app.Features.AddBusiness(_business);
        app.Features.AddCachings(_cachings);
        app.Features.AddCodingStyles(
        [
            c => c.AddRemoveChild(),
            _commandPattern,
            c => c.Id(),
            _initializable,
            _label,
            c => c.Locatable(),
            c => c.NamespaceAsRoute(),
            c => c.Query(),
            c => c.RecordsAreDtos(),
            c => c.RemainingServicesAreSingleton(),
            c => c.RichTransient(),
            _scopedBySuffix,
            _useBuiltInTypes,
            c => c.UseNullableTypes(),
            c => c.ValueType()
        ]);

        app.Features.AddCore(_core);
        app.Features.AddDatabase(_database);
        app.Features.AddExceptionHandling(_exceptionHandling);
        if (_greeting is not null) { app.Features.AddGreeting(_greeting); }
        app.Features.AddLifetimes(
        [
            c => c.Scoped(),
            c => c.Singleton(),
            c => c.Transient()
        ]);
        app.Features.AddLocalization(_localization);
        if (_logging is not null) { app.Features.AddLogging(_logging); }
        app.Features.AddReporting(_reporting);
        if (_rateLimiter is not null) { app.Features.AddRateLimiter(_rateLimiter); }

        if (_mockOverrider is not null) { app.Features.AddMockOverrider(_mockOverrider); }

        _configure(app);
    }
}