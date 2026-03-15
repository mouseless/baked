using Baked.Architecture;
using Baked.Binding;
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

namespace Baked.DataSource;

public abstract class DataSourceRecipe
{
    FeatureFunc<BusinessConfigurator> _business;

    IEnumerable<FeatureFunc<BindingConfigurator>> _bindings = [c => c.Rest()];
    public void Bindings(params IEnumerable<FeatureFunc<BindingConfigurator>> bindings) => _bindings = bindings;

    IEnumerable<FeatureFunc<CachingConfigurator>> _cachings = [c => c.InMemory(), c => c.ScopedMemory()];
    public void Cachings(params IEnumerable<FeatureFunc<CachingConfigurator>> cachings) => _cachings = cachings;

    IEnumerable<FeatureFunc<CodingStyleConfigurator>> _codingStyles;

    FeatureFunc<ExceptionHandlingConfigurator> _exceptionHandling = c => c.ProblemDetails();
    public void ExceptionHandling(FeatureFunc<ExceptionHandlingConfigurator> exceptionHandling) => _exceptionHandling = exceptionHandling;

    IEnumerable<FeatureFunc<Lifetime.LifetimeConfigurator>> _lifetimes = [c => c.Singleton(), c => c.Scoped(), c => c.Transient()];
    public void Lifetimes(IEnumerable<FeatureFunc<Lifetime.LifetimeConfigurator>> lifetimes) => _lifetimes = lifetimes;

    FeatureFunc<LocalizationConfigurator> _localization = c => c.Dotnet();
    public void Localization(FeatureFunc<LocalizationConfigurator> localization) => _localization = localization;

    FeatureFunc<CodingStyleConfigurator> _commandPattern = c => c.CommandPattern();
    public void CommandPattern(Func<CodingStyleConfigurator, CommandPatternCodingStyleFeature> commandPattern) => _commandPattern = c => commandPattern(c);

    FeatureFunc<CodingStyleConfigurator> _initializable = c => c.Initializable();
    public void Initializable(Func<CodingStyleConfigurator, InitializableCodingStyleFeature> initializable) => _initializable = c => initializable(c);

    FeatureFunc<CodingStyleConfigurator> _label = c => c.Label();
    public void Label(Func<CodingStyleConfigurator, LabelCodingStyleFeature> label) => _label = c => label(c);

    FeatureFunc<CodingStyleConfigurator> _scopedBySuffix = c => c.ScopedBySuffix();
    public void ScopedBySuffix(Func<CodingStyleConfigurator, ScopedBySuffixCodingStyleFeature> scopedBySuffix) => _scopedBySuffix = c => scopedBySuffix(c);

    FeatureFunc<CodingStyleConfigurator> _useBuiltInTypes = c => c.UseBuiltInTypes();
    public void UseBuiltInTypes(Func<CodingStyleConfigurator, UseBuiltInTypesCodingStyleFeature> useBuiltInTypes) => _useBuiltInTypes = c => useBuiltInTypes(c);

    Action<ApplicationDescriptor> _configure = _ => { };
    public void Configure(Action<ApplicationDescriptor> configure) => _configure = configure;

    public DataSourceRecipe(FeatureFunc<BusinessConfigurator> business)
    {
        _business = business;
        _codingStyles =
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
        ];
    }

    public class Run(FeatureFunc<BusinessConfigurator> business)
        : DataSourceRecipe(business)
    {
        FeatureFunc<CoreConfigurator> _core = c => c.Dotnet();
        public void Core(FeatureFunc<CoreConfigurator> core) => _core = core;

        FeatureFunc<DatabaseConfigurator> _database = c => c.Sqlite();
        public void Database(FeatureFunc<DatabaseConfigurator> database) => _database = database;

        FeatureFunc<GreetingConfigurator> _greeting = c => c.Swagger();
        public void Greeting(FeatureFunc<GreetingConfigurator> greeting) => _greeting = greeting;

        FeatureFunc<LoggingConfigurator> _logging = c => c.Request();
        public void Logging(FeatureFunc<LoggingConfigurator> logging) => _logging = logging;

        FeatureFunc<RateLimiterConfigurator> _rateLimiter = c => c.Concurrency();
        public void RateLimiter(FeatureFunc<RateLimiterConfigurator> rateLimiter) => _rateLimiter = rateLimiter;

        FeatureFunc<ReportingConfigurator> _reporting = c => c.NativeSql();
        public void Reporting(FeatureFunc<ReportingConfigurator> reporting) => _reporting = reporting;

        public override void Apply(ApplicationDescriptor app)
        {
            app.Layers.AddCodeGeneration();
            app.Layers.AddDataAccess();
            app.Layers.AddDomain();
            app.Layers.AddHttpServer();
            app.Layers.AddRestApi();
            app.Layers.AddRuntime();

            app.Features.AddBindings(_bindings);
            app.Features.AddBusiness(_business);
            app.Features.AddCachings(_cachings);
            app.Features.AddCodingStyles(_codingStyles);
            app.Features.AddCore(_core);
            app.Features.AddDatabase(_database);
            app.Features.AddExceptionHandling(_exceptionHandling);
            app.Features.AddGreeting(_greeting);
            app.Features.AddLifetimes(_lifetimes);
            app.Features.AddLocalization(_localization);
            app.Features.AddLogging(_logging);
            app.Features.AddRateLimiter(_rateLimiter);
            app.Features.AddReporting(_reporting);

            _configure(app);
        }
    }

    public class Test(FeatureFunc<BusinessConfigurator> business)
        : DataSourceRecipe(business)
    {
        FeatureFunc<CoreConfigurator> _core = c => c.Mock();
        public void Core(FeatureFunc<CoreConfigurator> core) => _core = core;

        FeatureFunc<DatabaseConfigurator> _database = c => c.InMemory();
        public void Database(FeatureFunc<DatabaseConfigurator> database) => _database = database;

        FeatureFunc<MockOverriderConfigurator> _mockOverrider = c => c.FirstInterface();
        public void MockOverrider(FeatureFunc<MockOverriderConfigurator> mockOverrider) => _mockOverrider = mockOverrider;

        FeatureFunc<ReportingConfigurator> _reporting = c => c.Mock();
        public void Reporting(FeatureFunc<ReportingConfigurator> reporting) => _reporting = reporting;

        public override void Apply(ApplicationDescriptor app)
        {
            app.Layers.AddCodeGeneration();
            app.Layers.AddDataAccess();
            app.Layers.AddDomain();
            app.Layers.AddRuntime();
            app.Layers.AddTesting();

            app.Features.AddBindings(_bindings);
            app.Features.AddBusiness(_business);
            app.Features.AddCachings(_cachings);
            app.Features.AddCodingStyles(_codingStyles);
            app.Features.AddCore(_core);
            app.Features.AddDatabase(_database);
            app.Features.AddExceptionHandling(_exceptionHandling);
            app.Features.AddLifetimes(_lifetimes);
            app.Features.AddLocalization(_localization);
            app.Features.AddMockOverrider(_mockOverrider);
            app.Features.AddReporting(_reporting);

            _configure(app);
        }
    }

    public abstract void Apply(ApplicationDescriptor app);
}