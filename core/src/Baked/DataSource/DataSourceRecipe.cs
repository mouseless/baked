using Baked.Architecture;

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

    FeatureFunc<ExceptionHandlingFeature.Configurator> _exceptionHandling = c => c.ProblemDetails();
    public void ExceptionHandling(FeatureFunc<ExceptionHandlingFeature.Configurator> exceptionHandling) => _exceptionHandling = exceptionHandling;

    IEnumerable<FeatureFunc<LifetimeFeature.Configurator>> _lifetimes = [c => c.Application(), c => c.Scope(), c => c.Instance()];
    public void Lifetimes(IEnumerable<FeatureFunc<LifetimeFeature.Configurator>> lifetimes) => _lifetimes = lifetimes;

    FeatureFunc<LocalizationFeature.Configurator> _localization = c => c.Dotnet();
    public void Localization(FeatureFunc<LocalizationFeature.Configurator> localization) => _localization = localization;

    // Coding Styles
    FeatureFunc<CodingStyleFeature.Configurator> _commandViaMethodName = c => c.CommandViaMethodName();
    public void CommandViaMethodName(FeatureFunc<CodingStyleFeature.Configurator> commandViaMethodName) => _commandViaMethodName = commandViaMethodName;

    FeatureFunc<CodingStyleFeature.Configurator> _initializableViaMethodName = c => c.InitializableViaMethodName();
    public void InitializableViaMethodName(FeatureFunc<CodingStyleFeature.Configurator> initializableViaMethodName) => _initializableViaMethodName = initializableViaMethodName;

    FeatureFunc<CodingStyleFeature.Configurator> _nameBasedLabel = c => c.NameBasedLabel();
    public void NameBasedLabel(FeatureFunc<CodingStyleFeature.Configurator> nameBasedLabel) => _nameBasedLabel = nameBasedLabel;

    FeatureFunc<CodingStyleFeature.Configurator> _scopedViaSuffix = c => c.ScopedViaSuffix();
    public void ScopedViaSuffix(FeatureFunc<CodingStyleFeature.Configurator> scopedViaSuffix) => _scopedViaSuffix = scopedViaSuffix;

    FeatureFunc<CodingStyleFeature.Configurator> _useBuiltInTypes = c => c.UseBuiltInTypes();
    public void UseBuiltInTypes(FeatureFunc<CodingStyleFeature.Configurator> useBuiltInTypes) => _useBuiltInTypes = useBuiltInTypes;

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
        FeatureFunc<CoreFeature.Configurator> _core = c => c.Dotnet();
        public void Core(FeatureFunc<CoreFeature.Configurator> core) => _core = core;

        FeatureFunc<DatabaseFeature.Configurator> _database = c => c.Sqlite();
        public void Database(FeatureFunc<DatabaseFeature.Configurator> database) => _database = database;

        FeatureFunc<GreetingFeature.Configurator> _greeting = c => c.Swagger();
        public void Greeting(FeatureFunc<GreetingFeature.Configurator> greeting) => _greeting = greeting;

        FeatureFunc<LoggingFeature.Configurator> _logging = c => c.Request();
        public void Logging(FeatureFunc<LoggingFeature.Configurator> logging) => _logging = logging;

        FeatureFunc<RateLimiterFeature.Configurator> _rateLimiter = c => c.Concurrency();
        public void RateLimiter(FeatureFunc<RateLimiterFeature.Configurator> rateLimiter) => _rateLimiter = rateLimiter;

        FeatureFunc<ReportingFeature.Configurator> _reporting = c => c.NativeSql();
        public void Reporting(FeatureFunc<ReportingFeature.Configurator> reporting) => _reporting = reporting;

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
        FeatureFunc<CoreFeature.Configurator> _core = c => c.Mock();
        public void Core(FeatureFunc<CoreFeature.Configurator> core) => _core = core;

        FeatureFunc<DatabaseFeature.Configurator> _database = c => c.InMemory();
        public void Database(FeatureFunc<DatabaseFeature.Configurator> database) => _database = database;

        FeatureFunc<MockOverriderFeature.Configurator> _mockOverrider = c => c.FirstInterface();
        public void MockOverrider(FeatureFunc<MockOverriderFeature.Configurator> mockOverrider) => _mockOverrider = mockOverrider;

        FeatureFunc<ReportingFeature.Configurator> _reporting = c => c.Mock();
        public void Reporting(FeatureFunc<ReportingFeature.Configurator> reporting) => _reporting = reporting;

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