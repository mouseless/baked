using Baked.Architecture;
using Baked.Authentication;
using Baked.Authorization;
using Baked.Business;
using Baked.Caching;
using Baked.CodingStyle;
using Baked.CodingStyle.CommandPattern;
using Baked.CodingStyle.Initializable;
using Baked.CodingStyle.Label;
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

namespace Baked.Recipe.Monolith;

public abstract class MonolithRecipe(Func<BusinessConfigurator, IFeature<BusinessConfigurator>> _business, ExecutionMode _mode)
{
    public class Test(Func<BusinessConfigurator, IFeature<BusinessConfigurator>> _business) : MonolithRecipe(_business, ExecutionMode.Test);
    public class Run : MonolithRecipe
    {
        public Run(Func<BusinessConfigurator, IFeature<BusinessConfigurator>> business) : base(business, ExecutionMode.Run)
        {
            Authentications(c => c.FixedBearerToken());
            Authorization(c => c.ClaimBased());
            Communication(c => c.Http());
            Core(c => c.Dotnet());
            Cors(c => c.Disabled());
            Database(c => c.Sqlite());
            Greeting(c => c.Swagger());
            Logging(c => c.Request());
            MockOverrider(c => c.FirstInterface());
            RateLimiter(c => c.Concurrency());
        }
    }

    Func<BusinessConfigurator, IFeature<BusinessConfigurator>> _business = _business;
    IEnumerable<Func<AuthenticationConfigurator, IFeature<AuthenticationConfigurator>>> _authentications = [];
    Func<AuthorizationConfigurator, IFeature<AuthorizationConfigurator>>? _authorization = default;
    IEnumerable<Func<CachingConfigurator, IFeature<CachingConfigurator>>> _cachings = [c => c.InMemory(), c => c.ScopedMemory()];
    Func<CommunicationConfigurator, IFeature<CommunicationConfigurator>> _communication = c => c.Mock();
    Func<CoreConfigurator, IFeature<CoreConfigurator>> _core = c => c.Mock();
    Func<CorsConfigurator, IFeature<CorsConfigurator>>? _cors = default;
    Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>> _database = c => c.InMemory();
    Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>> _exceptionHandling = c => c.ProblemDetails();
    Func<GreetingConfigurator, IFeature<GreetingConfigurator>>? _greeting = default;
    Func<LocalizationConfigurator, IFeature<LocalizationConfigurator>> _localization = c => c.Dotnet();
    Func<LoggingConfigurator, IFeature<LoggingConfigurator>>? _logging = default;
    Func<MockOverriderConfigurator, IFeature<MockOverriderConfigurator>>? _mockOverrider = default;
    Func<OrmConfigurator, IFeature<OrmConfigurator>> _orm = c => c.AutoMap();
    Func<RateLimiterConfigurator, IFeature<RateLimiterConfigurator>>? _rateLimiter = default;
    Func<ThemeConfigurator, IFeature<ThemeConfigurator>>? _theme = default;

    Func<CodingStyleConfigurator, CommandPatternCodingStyleFeature> _commandPattern = c => c.CommandPattern();
    Func<CodingStyleConfigurator, InitializableCodingStyleFeature> _initializable = c => c.Initializable();
    Func<CodingStyleConfigurator, LabelCodingStyleFeature> _label = c => c.Label();
    Func<CodingStyleConfigurator, ScopedBySuffixCodingStyleFeature> _scopedBySuffix = c => c.ScopedBySuffix();
    Func<CodingStyleConfigurator, UseBuiltInTypesCodingStyleFeature> _useBuiltInTypes = c => c.UseBuiltInTypes();

    Action<ApplicationDescriptor> _configure = _ => { };

    public void Authentications(params Func<AuthenticationConfigurator, IFeature<AuthenticationConfigurator>>[] authentications) =>
        _authentications = authentications;

    public void Authorization(Func<AuthorizationConfigurator, IFeature<AuthorizationConfigurator>>? authorization) =>
        _authorization = authorization;

    public void Cachings(params Func<CachingConfigurator, IFeature<CachingConfigurator>>[] cachings) =>
        _cachings = cachings;

    public void Communication(Func<CommunicationConfigurator, IFeature<CommunicationConfigurator>> communication) =>
        _communication = communication;

    public void Core(Func<CoreConfigurator, IFeature<CoreConfigurator>> core) =>
        _core = core;

    public void Cors(Func<CorsConfigurator, IFeature<CorsConfigurator>>? cors) =>
        _cors = cors;

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

    public void Orm(Func<OrmConfigurator, IFeature<OrmConfigurator>> orm) =>
        _orm = orm;

    public void RateLimiter(Func<RateLimiterConfigurator, IFeature<RateLimiterConfigurator>>? rateLimiter) =>
        _rateLimiter = rateLimiter;

    public void Theme(Func<ThemeConfigurator, IFeature<ThemeConfigurator>>? theme) =>
        _theme = theme;

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
        if (_mode == ExecutionMode.Run) { app.Layers.AddHttpClient(); }
        if (_mode == ExecutionMode.Run) { app.Layers.AddHttpServer(); }
        if (_mode == ExecutionMode.Run) { app.Layers.AddRestApi(); }

        app.Features.AddAuthentications(_authentications);
        if (_authorization is not null) { app.Features.AddAuthorization(_authorization); }
        app.Features.AddBinding(c => c.Rest());
        app.Features.AddBusiness(_business);
        app.Features.AddCachings(_cachings);
        app.Features.AddCodingStyles(
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
        ]);

        app.Features.AddCommunication(_communication);
        app.Features.AddCore(_core);
        if (_cors is not null) { app.Features.AddCors(_cors); }
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
        app.Features.AddOrm(_orm);
        if (_rateLimiter is not null) { app.Features.AddRateLimiter(_rateLimiter); }

        if (_mockOverrider is not null) { app.Features.AddMockOverrider(_mockOverrider); }

        if (_theme is not null)
        {
            if (_mode == ExecutionMode.Run) { app.Layers.AddUi(); }

            app.Features.AddUx(
            [
                c => c.ActionsAsButtons(),
                c => c.ActionsAreContents(),
                c => c.ActionsAsDataPanels(),
                c => c.DataTableDefaults(),
                c => c.DescriptionProperty(),
                c => c.EnumParameterIsSelect(),
                c => c.InitializerParametersAreInPageTitle(),
                c => c.LabelsAreFrozen(),
                c => c.ListIsDataTable(),
                c => c.NumericValuesAreFormatted(),
                c => c.ObjectWithListIsDataTable(),
                c => c.PanelParametersAreStateful(),
                c => c.PropertiesAsFieldset(),
                c => c.RoutedTypesAsNavLinks()
            ]);

            app.Features.AddTheme(_theme);
        }

        _configure(app);
    }
}