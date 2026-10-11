using Baked.Architecture;
using Baked.Authentication;
using Baked.Authorization;
using Baked.Binding;
using Baked.Business;
using Baked.Caching;
using Baked.CodingStyle;
using Baked.CodingStyle.CommandViaMethodName;
using Baked.CodingStyle.InitializableViaMethodName;
using Baked.CodingStyle.NameBasedLabel;
using Baked.CodingStyle.QueryViaPluralName;
using Baked.CodingStyle.ScopedViaSuffix;
using Baked.CodingStyle.UseBuiltInTypes;
using Baked.Communication;
using Baked.Core;
using Baked.Cors;
using Baked.Database;
using Baked.ExceptionHandling;
using Baked.Greeting;
using Baked.Lifetime;
using Baked.Localization;
using Baked.Logging;
using Baked.MockOverrider;
using Baked.Orm;
using Baked.RateLimiter;
using Baked.Theme;
using Baked.Ux;
using Baked.Ux.EnumParameterIsSelect;
using Baked.Ux.QueryActionAsDataContainer;

namespace Baked.Monolith;

public abstract class MonolithRecipe(FeatureFunc<BusinessConfigurator> business)
{
    // Features
    FeatureFunc<BusinessConfigurator> _business = business;

    IEnumerable<FeatureFunc<BindingConfigurator>> _bindings = [c => c.Rest()];
    public void Bindings(params IEnumerable<FeatureFunc<BindingConfigurator>> bindings) => _bindings = bindings;

    IEnumerable<FeatureFunc<CachingConfigurator>> _cachings = [c => c.InMemory(), c => c.ScopedMemory()];
    public void Cachings(params IEnumerable<FeatureFunc<CachingConfigurator>> cachings) => _cachings = cachings;

    IEnumerable<FeatureFunc<CodingStyleConfigurator>>? _codingStyles;
    public void CodingStyles(IEnumerable<FeatureFunc<CodingStyleConfigurator>> codingStyles) => _codingStyles = codingStyles;

    FeatureFunc<ExceptionHandlingConfigurator> _exceptionHandling = c => c.ProblemDetails();
    public void ExceptionHandling(FeatureFunc<ExceptionHandlingConfigurator> exceptionHandling) => _exceptionHandling = exceptionHandling;

    IEnumerable<FeatureFunc<LifetimeConfigurator>> _lifetimes = [c => c.Scope(), c => c.Application(), c => c.Instance()];
    public void Lifetimes(IEnumerable<FeatureFunc<LifetimeConfigurator>> lifetimes) => _lifetimes = lifetimes;

    FeatureFunc<LocalizationConfigurator> _localization = c => c.Dotnet();
    public void Localization(FeatureFunc<LocalizationConfigurator> localization) => _localization = localization;

    FeatureFunc<OrmConfigurator> _orm = c => c.AutoMap();
    public void Orm(FeatureFunc<OrmConfigurator> orm) => _orm = orm;

    // Coding Styles
    FeatureFunc<CodingStyleConfigurator> _commandViaMethodName = c => c.CommandViaMethodName();
    public void CommandViaMethodName(Func<CodingStyleConfigurator, CommandViaMethodNameCodingStyleFeature> commandViaMethodName) => _commandViaMethodName = c => commandViaMethodName(c);

    FeatureFunc<CodingStyleConfigurator> _initializableViaMethodName = c => c.InitializableViaMethodName();
    public void InitializableViaMethodName(Func<CodingStyleConfigurator, InitializableViaMethodNameCodingStyleFeature> initializableViaMethodName) => _initializableViaMethodName = c => initializableViaMethodName(c);

    FeatureFunc<CodingStyleConfigurator> _nameBasedLabel = c => c.NameBasedLabel();
    public void NameBasedLabel(Func<CodingStyleConfigurator, NameBasedLabelCodingStyleFeature> nameBasedLabel) => _nameBasedLabel = c => nameBasedLabel(c);

    FeatureFunc<CodingStyleConfigurator> _queryViaPluralName = c => c.QueryViaPluralName();
    public void QueryViaPluralName(Func<CodingStyleConfigurator, QueryViaPluralNameCodingStyleFeature> queryViaPluralName) => _queryViaPluralName = c => queryViaPluralName(c);

    FeatureFunc<CodingStyleConfigurator> _scopedViaSuffix = c => c.ScopedViaSuffix();
    public void ScopedViaSuffix(Func<CodingStyleConfigurator, ScopedViaSuffixCodingStyleFeature> scopedViaSuffix) => _scopedViaSuffix = c => scopedViaSuffix(c);

    FeatureFunc<CodingStyleConfigurator> _useBuiltInTypes = c => c.UseBuiltInTypes();
    public void UseBuiltInTypes(Func<CodingStyleConfigurator, UseBuiltInTypesCodingStyleFeature> useBuiltInTypes) => _useBuiltInTypes = c => useBuiltInTypes(c);

    // Configure
    Action<ApplicationDescriptor> _configure = _ => { };
    public void Configure(Action<ApplicationDescriptor> configure) => _configure = configure;

    IEnumerable<FeatureFunc<CodingStyleConfigurator>> CodingStyleFeatures => _codingStyles ??
    [
        c => c.AddRemoveChildAsSubResource(),
        _commandViaMethodName,
        c => c.ExtensionViaLocatableInitializer(),
        c => c.FlagsEnum(),
        _initializableViaMethodName,
        c => c.LocateViaId(),
        _nameBasedLabel,
        c => c.NamespaceAsRoute(),
        c => c.ObjectAsJson(),
        c => c.PrimitiveViaParsable(),
        _queryViaPluralName,
        c => c.RecordsAreDtos(),
        c => c.RemainingServicesAreSingleton(),
        c => c.ResourceViaIdInitializer(),
        c => c.RichEntity(),
        _scopedViaSuffix,
        c => c.SuffixBasedClient(),
        c => c.TypeBasedId(),
        c => c.UniqueViaSingleBy(),
        c => c.UriReturnIsRedirect(),
        _useBuiltInTypes,
        c => c.UseNullableTypes()
    ];

    public class Run(FeatureFunc<BusinessConfigurator> business)
        : MonolithRecipe(business)
    {
        IEnumerable<FeatureFunc<AuthenticationConfigurator>> _authentications = [c => c.FixedBearerToken()];
        public void Authentications(params IEnumerable<FeatureFunc<AuthenticationConfigurator>> authentications) => _authentications = authentications;

        FeatureFunc<AuthorizationConfigurator> _authorization = c => c.ClaimBased();
        public void Authorization(FeatureFunc<AuthorizationConfigurator> authorization) => _authorization = authorization;

        FeatureFunc<CommunicationConfigurator> _communication = c => c.Http();
        public void Communication(FeatureFunc<CommunicationConfigurator> communication) => _communication = communication;

        FeatureFunc<CoreConfigurator> _core = c => c.Dotnet();
        public void Core(FeatureFunc<CoreConfigurator> core) => _core = core;

        FeatureFunc<CorsConfigurator> _cors = c => c.Disabled();
        public void Cors(FeatureFunc<CorsConfigurator> cors) => _cors = cors;

        FeatureFunc<DatabaseConfigurator> _database = c => c.Sqlite();
        public void Database(FeatureFunc<DatabaseConfigurator> database) => _database = database;

        FeatureFunc<GreetingConfigurator> _greeting = c => c.Swagger();
        public void Greeting(FeatureFunc<GreetingConfigurator> greeting) => _greeting = greeting;

        FeatureFunc<LoggingConfigurator> _logging = c => c.Request();
        public void Logging(FeatureFunc<LoggingConfigurator> logging) => _logging = logging;

        FeatureFunc<RateLimiterConfigurator> _rateLimiter = c => c.Concurrency();
        public void RateLimiter(FeatureFunc<RateLimiterConfigurator> rateLimiter) => _rateLimiter = rateLimiter;

        FeatureFunc<ThemeConfigurator>? _theme = default;
        public void Theme(FeatureFunc<ThemeConfigurator>? theme) => _theme = theme;

        FeatureFunc<UxConfigurator> _enumParameterIsSelect = c => c.EnumParameterIsSelect();
        public void EnumParameterIsSelect(Func<UxConfigurator, EnumParameterIsSelectUxFeature> enumParameterIsSelect) => _enumParameterIsSelect = c => enumParameterIsSelect(c);

        FeatureFunc<UxConfigurator> _queryActionAsDataContainer = c => c.QueryActionAsDataContainer();
        public void QueryActionAsDataContainer(Func<UxConfigurator, QueryActionAsDataContainerUxFeature> queryActionAsDataContainer) => _queryActionAsDataContainer = c => queryActionAsDataContainer(c);

        public override void Apply(ApplicationDescriptor app)
        {
            app.Layers.AddBuildtime();
            app.Layers.AddDataAccess();
            app.Layers.AddDomain();
            app.Layers.AddHttpClient();
            app.Layers.AddHttpServer();
            app.Layers.AddRestApi();
            app.Layers.AddRuntime();

            app.Features.AddAuthentications(_authentications);
            app.Features.AddAuthorization(_authorization);
            app.Features.AddBindings(_bindings);
            app.Features.AddBusiness(_business);
            app.Features.AddCachings(_cachings);
            app.Features.AddCodingStyles(CodingStyleFeatures);
            app.Features.AddCommunication(_communication);
            app.Features.AddCore(_core);
            app.Features.AddCors(_cors);
            app.Features.AddDatabase(_database);
            app.Features.AddExceptionHandling(_exceptionHandling);
            app.Features.AddGreeting(_greeting);
            app.Features.AddLifetimes(_lifetimes);
            app.Features.AddLocalization(_localization);
            app.Features.AddLogging(_logging);
            app.Features.AddOrm(_orm);
            app.Features.AddRateLimiter(_rateLimiter);

            if (_theme is not null)
            {
                app.Layers.AddUi();

                app.Features.AddUx(
                [
                    c => c.ActionsAsButtons(),
                    c => c.ActionsAreContents(),
                    c => c.ActionsAsDataPanels(),
                    c => c.DataTableDefaults(),
                    c => c.DescriptionProperty(),
                    _enumParameterIsSelect,
                    c => c.FormInputsAreIftaLabel(),
                    c => c.InitializerParametersAreInPageTitle(),
                    c => c.LabelsAreFrozen(),
                    c => c.ListIsDataTable(),
                    c => c.NumericValuesAreFormatted(),
                    c => c.ObjectWithListIsDataTable(),
                    c => c.PanelParametersAreStateful(),
                    _queryActionAsDataContainer,
                    c => c.PropertiesAsFieldset(),
                    c => c.RoutedTypesAsNavLinks()
                ]);

                app.Features.AddTheme(_theme);
            }

            _configure(app);
        }
    }

    public class Test(FeatureFunc<BusinessConfigurator> _business)
        : MonolithRecipe(_business)
    {
        FeatureFunc<MockOverriderConfigurator> _mockOverrider = c => c.FirstInterface();
        public void MockOverrider(FeatureFunc<MockOverriderConfigurator> mockOverrider) => _mockOverrider = mockOverrider;

        FeatureFunc<CommunicationConfigurator> _communication = c => c.Mock();
        public void Communication(FeatureFunc<CommunicationConfigurator> communication) => _communication = communication;

        FeatureFunc<CoreConfigurator> _core = c => c.Mock();
        public void Core(FeatureFunc<CoreConfigurator> core) => _core = core;

        FeatureFunc<DatabaseConfigurator> _database = c => c.InMemory();
        public void Database(FeatureFunc<DatabaseConfigurator> database) => _database = database;

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
            app.Features.AddCommunication(_communication);
            app.Features.AddCore(_core);
            app.Features.AddDatabase(_database);
            app.Features.AddExceptionHandling(_exceptionHandling);
            app.Features.AddLifetimes(_lifetimes);
            app.Features.AddLocalization(_localization);
            app.Features.AddMockOverrider(_mockOverrider);
            app.Features.AddOrm(_orm);

            _configure(app);
        }
    }

    public abstract void Apply(ApplicationDescriptor app);
}