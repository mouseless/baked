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

public abstract class DataSourceRecipe(FeatureFunc<BusinessConfigurator> _business, ExecutionMode _mode)
{
    public class Test(FeatureFunc<BusinessConfigurator> _business) : DataSourceRecipe(_business, ExecutionMode.Test);
    public class Run : DataSourceRecipe
    {
        public Run(FeatureFunc<BusinessConfigurator> business) : base(business, ExecutionMode.Run)
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

    FeatureFunc<BusinessConfigurator> _business = _business;
    IEnumerable<FeatureFunc<CachingConfigurator>> _cachings = [c => c.InMemory(), c => c.ScopedMemory()];
    FeatureFunc<CoreConfigurator> _core = c => c.Mock();
    FeatureFunc<DatabaseConfigurator> _database = c => c.InMemory();
    FeatureFunc<ExceptionHandlingConfigurator> _exceptionHandling = c => c.ProblemDetails();
    FeatureFunc<GreetingConfigurator>? _greeting = default;
    FeatureFunc<LocalizationConfigurator> _localization = c => c.Dotnet();
    FeatureFunc<LoggingConfigurator>? _logging = default;
    FeatureFunc<MockOverriderConfigurator>? _mockOverrider = c => c.FirstInterface();
    FeatureFunc<RateLimiterConfigurator>? _rateLimiter = default;
    FeatureFunc<ReportingConfigurator> _reporting = c => c.Mock();

    Func<CodingStyleConfigurator, CommandPatternCodingStyleFeature> _commandPattern = c => c.CommandPattern();
    Func<CodingStyleConfigurator, InitializableCodingStyleFeature> _initializable = c => c.Initializable();
    Func<CodingStyleConfigurator, LabelCodingStyleFeature> _label = c => c.Label();
    Func<CodingStyleConfigurator, ScopedBySuffixCodingStyleFeature> _scopedBySuffix = c => c.ScopedBySuffix();
    Func<CodingStyleConfigurator, UseBuiltInTypesCodingStyleFeature> _useBuiltInTypes = c => c.UseBuiltInTypes();

    Action<ApplicationDescriptor> _configure = _ => { };

    public void Cachings(params FeatureFunc<CachingConfigurator>[] cachings) =>
        _cachings = cachings;

    public void Core(FeatureFunc<CoreConfigurator> core) =>
        _core = core;

    public void Database(FeatureFunc<DatabaseConfigurator> database) =>
        _database = database;

    public void ExceptionHandling(FeatureFunc<ExceptionHandlingConfigurator> exceptionHandling) =>
        _exceptionHandling = exceptionHandling;

    public void Greeting(FeatureFunc<GreetingConfigurator>? greeting) =>
        _greeting = greeting;

    public void Localization(FeatureFunc<LocalizationConfigurator> localization) =>
        _localization = localization;

    public void Logging(FeatureFunc<LoggingConfigurator>? logging) =>
        _logging = logging;

    public void MockOverrider(FeatureFunc<MockOverriderConfigurator>? mockOverrider) =>
        _mockOverrider = mockOverrider;

    public void RateLimiter(FeatureFunc<RateLimiterConfigurator>? rateLimiter) =>
        _rateLimiter = rateLimiter;

    public void Reporting(FeatureFunc<ReportingConfigurator> reporting) =>
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