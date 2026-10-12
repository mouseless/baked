using Baked.Architecture;
using Baked.CodingStyle.CommandViaMethodName;
using Baked.CodingStyle.InitializableViaMethodName;
using Baked.CodingStyle.NameBasedLabel;
using Baked.CodingStyle.ScopedViaSuffix;
using Baked.CodingStyle.UseBuiltInTypes;
using Baked.Core;
using Baked.Database;
using Baked.ExceptionHandling;
using Baked.Greeting;
using Baked.Lifetime;
using Baked.Localization;
using Baked.Logging;
using Baked.MockOverrider;
using Baked.RateLimiter;
using Baked.Reporting;

namespace Baked.DataSource;

public abstract class DataSourceRecipe(FeatureFunc<BusinessFeature.Configurator> business)
{
    // Features
    FeatureFunc<BusinessFeature.Configurator> _business = business;

    IEnumerable<FeatureFunc<BindingFeature.Configurator>> _bindings = [c => c.Rest()];
    public void Bindings(params IEnumerable<FeatureFunc<BindingFeature.Configurator>> bindings) => _bindings = bindings;

    IEnumerable<FeatureFunc<CachingFeature.Configurator>> _cachings = [c => c.InMemory(), c => c.ScopedMemory()];
    public void Cachings(params IEnumerable<FeatureFunc<CachingFeature.Configurator>> cachings) => _cachings = cachings;

    IEnumerable<FeatureFunc<CodingStyleFeature.Configurator>>? _codingStyles;
    public void CodingStyles(IEnumerable<FeatureFunc<CodingStyleFeature.Configurator>> codingStyles) => _codingStyles = codingStyles;

    FeatureFunc<ExceptionHandlingConfigurator> _exceptionHandling = c => c.ProblemDetails();
    public void ExceptionHandling(FeatureFunc<ExceptionHandlingConfigurator> exceptionHandling) => _exceptionHandling = exceptionHandling;

    IEnumerable<FeatureFunc<LifetimeConfigurator>> _lifetimes = [c => c.Application(), c => c.Scope(), c => c.Instance()];
    public void Lifetimes(IEnumerable<FeatureFunc<LifetimeConfigurator>> lifetimes) => _lifetimes = lifetimes;

    FeatureFunc<LocalizationConfigurator> _localization = c => c.Dotnet();
    public void Localization(FeatureFunc<LocalizationConfigurator> localization) => _localization = localization;

    // Coding Styles
    FeatureFunc<CodingStyleFeature.Configurator> _commandViaMethodName = c => c.CommandViaMethodName();
    public void CommandViaMethodName(Func<CodingStyleFeature.Configurator, CommandViaMethodNameCodingStyleFeature> commandViaMethodName) => _commandViaMethodName = c => commandViaMethodName(c);

    FeatureFunc<CodingStyleFeature.Configurator> _initializableViaMethodName = c => c.InitializableViaMethodName();
    public void InitializableViaMethodName(Func<CodingStyleFeature.Configurator, InitializableViaMethodNameCodingStyleFeature> initializableViaMethodName) => _initializableViaMethodName = c => initializableViaMethodName(c);

    FeatureFunc<CodingStyleFeature.Configurator> _nameBasedLabel = c => c.NameBasedLabel();
    public void NameBasedLabel(Func<CodingStyleFeature.Configurator, NameBasedLabelCodingStyleFeature> nameBasedLabel) => _nameBasedLabel = c => nameBasedLabel(c);

    FeatureFunc<CodingStyleFeature.Configurator> _scopedViaSuffix = c => c.ScopedViaSuffix();
    public void ScopedViaSuffix(Func<CodingStyleFeature.Configurator, ScopedViaSuffixCodingStyleFeature> scopedViaSuffix) => _scopedViaSuffix = c => scopedViaSuffix(c);

    FeatureFunc<CodingStyleFeature.Configurator> _useBuiltInTypes = c => c.UseBuiltInTypes();
    public void UseBuiltInTypes(Func<CodingStyleFeature.Configurator, UseBuiltInTypesCodingStyleFeature> useBuiltInTypes) => _useBuiltInTypes = c => useBuiltInTypes(c);

    // Configure
    Action<ApplicationDescriptor> _configure = _ => { };
    public void Configure(Action<ApplicationDescriptor> configure) => _configure = configure;

    IEnumerable<FeatureFunc<CodingStyleFeature.Configurator>> CodingStyleFeatures => _codingStyles ??
    [
        c => c.AddRemoveChildAsSubResource(),
        _commandViaMethodName,
        c => c.FlagsEnum(),
        _initializableViaMethodName,
        c => c.LocateViaId(),
        _nameBasedLabel,
        c => c.NamespaceAsRoute(),
        c => c.PrimitiveViaParsable(),
        c => c.QueryViaPluralName(),
        c => c.RecordsAreDtos(),
        c => c.RemainingServicesAreSingleton(),
        c => c.ResourceViaIdInitializer(),
        _scopedViaSuffix,
        c => c.TypeBasedId(),
        _useBuiltInTypes,
        c => c.UseNullableTypes()
    ];

    public class Run(FeatureFunc<BusinessFeature.Configurator> business)
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
            app.Layers.AddBuildtime();
            app.Layers.AddDataAccess();
            app.Layers.AddDomain();
            app.Layers.AddHttpServer();
            app.Layers.AddRestApi();
            app.Layers.AddRuntime();

            app.Features.AddBindings(_bindings);
            app.Features.AddBusiness(_business);
            app.Features.AddCachings(_cachings);
            app.Features.AddCodingStyles(CodingStyleFeatures);
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

    public class Test(FeatureFunc<BusinessFeature.Configurator> business)
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
            app.Layers.AddBuildtime();
            app.Layers.AddDataAccess();
            app.Layers.AddDomain();
            app.Layers.AddRuntime();
            app.Layers.AddTesting();

            app.Features.AddBindings(_bindings);
            app.Features.AddBusiness(_business);
            app.Features.AddCachings(_cachings);
            app.Features.AddCodingStyles(CodingStyleFeatures);
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