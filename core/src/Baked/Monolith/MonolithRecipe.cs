using Baked.Architecture;
using Baked.Authentication;
using Baked.Authorization;
using Baked.Binding;
using Baked.Business;
using Baked.Caching;
using Baked.CodingStyle;
using Baked.CodingStyle.CommandPattern;
using Baked.CodingStyle.Initializable;
using Baked.CodingStyle.Label;
using Baked.CodingStyle.QueryMethod;
using Baked.CodingStyle.ScopedBySuffix;
using Baked.CodingStyle.UseBuiltInTypes;
using Baked.Communication;
using Baked.Core;
using Baked.Cors;
using Baked.Database;
using Baked.ExceptionHandling;
using Baked.Greeting;
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

public abstract class MonolithRecipe
{
    // Features
    FeatureFunc<BusinessConfigurator> _business;

    IEnumerable<FeatureFunc<BindingConfigurator>> _bindings = [c => c.Rest()];
    public void Bindings(params IEnumerable<FeatureFunc<BindingConfigurator>> bindings) => _bindings = bindings;

    IEnumerable<FeatureFunc<CachingConfigurator>> _cachings = [c => c.InMemory(), c => c.ScopedMemory()];
    public void Cachings(params IEnumerable<FeatureFunc<CachingConfigurator>> cachings) => _cachings = cachings;

    IEnumerable<FeatureFunc<CodingStyleConfigurator>> _codingStyles;
    public void CodingStyles(IEnumerable<FeatureFunc<CodingStyleConfigurator>> codingStyles) => _codingStyles = codingStyles;

    FeatureFunc<ExceptionHandlingConfigurator> _exceptionHandling = c => c.ProblemDetails();
    public void ExceptionHandling(FeatureFunc<ExceptionHandlingConfigurator> exceptionHandling) => _exceptionHandling = exceptionHandling;

    IEnumerable<FeatureFunc<Lifetime.LifetimeConfigurator>> _lifetimes = [c => c.Scoped(), c => c.Singleton(), c => c.Transient()];
    public void Lifetimes(IEnumerable<FeatureFunc<Lifetime.LifetimeConfigurator>> lifetimes) => _lifetimes = lifetimes;

    FeatureFunc<LocalizationConfigurator> _localization = c => c.Dotnet();
    public void Localization(FeatureFunc<LocalizationConfigurator> localization) => _localization = localization;

    FeatureFunc<OrmConfigurator> _orm = c => c.AutoMap();
    public void Orm(FeatureFunc<OrmConfigurator> orm) => _orm = orm;

    // Coding Styles
    FeatureFunc<CodingStyleConfigurator> _commandPattern = c => c.CommandPattern();
    public void CommandPattern(Func<CodingStyleConfigurator, CommandPatternCodingStyleFeature> commandPattern) => _commandPattern = c => commandPattern(c);

    FeatureFunc<CodingStyleConfigurator> _initializable = c => c.Initializable();
    public void Initializable(Func<CodingStyleConfigurator, InitializableCodingStyleFeature> initializable) => _initializable = c => initializable(c);

    FeatureFunc<CodingStyleConfigurator> _label = c => c.Label();
    public void Label(Func<CodingStyleConfigurator, LabelCodingStyleFeature> label) => _label = c => label(c);

    FeatureFunc<CodingStyleConfigurator> _queryMethod = c => c.QueryMethod();
    public void QueryMethod(Func<CodingStyleConfigurator, QueryMethodCodingStyleFeature> queryMethod) => _queryMethod = c => queryMethod(c);

    FeatureFunc<CodingStyleConfigurator> _scopedBySuffix = c => c.ScopedBySuffix();
    public void ScopedBySuffix(Func<CodingStyleConfigurator, ScopedBySuffixCodingStyleFeature> scopedBySuffix) => _scopedBySuffix = c => scopedBySuffix(c);

    FeatureFunc<CodingStyleConfigurator> _useBuiltInTypes = c => c.UseBuiltInTypes();
    public void UseBuiltInTypes(Func<CodingStyleConfigurator, UseBuiltInTypesCodingStyleFeature> useBuiltInTypes) => _useBuiltInTypes = c => useBuiltInTypes(c);

    // Configure
    Action<ApplicationDescriptor> _configure = _ => { };
    public void Configure(Action<ApplicationDescriptor> configure) => _configure = configure;

    public MonolithRecipe(FeatureFunc<BusinessConfigurator> business)
    {
        _business = business;
        _codingStyles =
        [
                c => c.AddRemoveChild(),
                c => c.Client(),
                _commandPattern,
                c => c.EntitySubclass(),
                c => c.Id(),
                _initializable,
                _label,
                c => c.Locatable(),
                c => c.LocatableExtension(),
                c => c.NamespaceAsRoute(),
                c => c.ObjectAsJson(),
                c => c.Query(),
                _queryMethod,
                c => c.RecordsAreDtos(),
                c => c.RemainingServicesAreSingleton(),
                c => c.RichEntity(),
                c => c.RichTransient(),
                _scopedBySuffix,
                c => c.Unique(),
                c => c.UriReturnIsRedirect(),
                _useBuiltInTypes,
                c => c.UseNullableTypes(),
                c => c.ValueType()
        ];
    }

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
            app.Features.AddCodingStyles(_codingStyles);
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
            app.Features.AddCodingStyles(_codingStyles);
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