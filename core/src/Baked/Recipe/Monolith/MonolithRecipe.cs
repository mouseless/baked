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

public class MonolithRecipe(Func<BusinessConfigurator, IFeature<BusinessConfigurator>> _business, ExecutionMode mode = ExecutionMode.Run)
{
    public ExecutionMode Mode { get; } = mode;
    public Func<BusinessConfigurator, IFeature<BusinessConfigurator>> Business { get; set; } = _business;
    public IEnumerable<Func<AuthenticationConfigurator, IFeature<AuthenticationConfigurator>>> Authentications { get; set; } = mode == ExecutionMode.Run ? [c => c.FixedBearerToken()] : [];
    public Func<AuthorizationConfigurator, IFeature<AuthorizationConfigurator>>? Authorization { get; set; } = mode == ExecutionMode.Run ? c => c.ClaimBased() : default;
    public IEnumerable<Func<CachingConfigurator, IFeature<CachingConfigurator>>> Cachings { get; set; } = [c => c.InMemory(), c => c.ScopedMemory()];
    public Func<CommunicationConfigurator, IFeature<CommunicationConfigurator>> Communication { get; set; } = mode == ExecutionMode.Run ? c => c.Http() : c => c.Mock();
    public Func<CoreConfigurator, IFeature<CoreConfigurator>> Core { get; set; } = mode == ExecutionMode.Run ? c => c.Dotnet() : c => c.Mock();
    public Func<CorsConfigurator, IFeature<CorsConfigurator>>? Cors { get; set; } = mode == ExecutionMode.Run ? c => c.Disabled() : default;
    public Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>> Database { get; set; } = mode == ExecutionMode.Run ? c => c.Sqlite() : c => c.InMemory();
    public Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>> ExceptionHandling { get; set; } = c => c.ProblemDetails();
    public Func<GreetingConfigurator, IFeature<GreetingConfigurator>>? Greeting { get; set; } = mode == ExecutionMode.Run ? c => c.Swagger() : default;
    public Func<LocalizationConfigurator, IFeature<LocalizationConfigurator>> Localization { get; set; } = c => c.Dotnet();
    public Func<LoggingConfigurator, IFeature<LoggingConfigurator>>? Logging { get; set; } = mode == ExecutionMode.Run ? c => c.Request() : default;
    public Func<MockOverriderConfigurator, IFeature<MockOverriderConfigurator>>? MockOverrider { get; set; } = mode == ExecutionMode.Test ? c => c.FirstInterface() : default;
    public Func<OrmConfigurator, IFeature<OrmConfigurator>> Orm { get; set; } = c => c.AutoMap();
    public Func<RateLimiterConfigurator, IFeature<RateLimiterConfigurator>>? RateLimiter { get; set; } = mode == ExecutionMode.Run ? c => c.Concurrency() : default;
    public Func<ThemeConfigurator, IFeature<ThemeConfigurator>>? Theme { get; set; } = default;

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
        if (Mode == ExecutionMode.Run) { app.Layers.AddHttpClient(); }
        if (Mode == ExecutionMode.Run) { app.Layers.AddHttpServer(); }
        if (Mode == ExecutionMode.Run) { app.Layers.AddRestApi(); }

        app.Features.AddAuthentications(Authentications);
        if (Authorization is not null) { app.Features.AddAuthorization(Authorization); }
        app.Features.AddBinding(c => c.Rest());
        app.Features.AddBusiness(Business);
        app.Features.AddCachings(Cachings);
        app.Features.AddCodingStyles(
        [
            c => c.AddRemoveChild(),
            c => c.Client(),
            CommandPattern,
            c => c.EntitySubclass(),
            c => c.Id(),
            Initializable,
            Label,
            c => c.Locatable(),
            c => c.LocatableExtension(),
            c => c.NamespaceAsRoute(),
            c => c.ObjectAsJson(),
            c => c.Query(),
            c => c.RecordsAreDtos(),
            c => c.RemainingServicesAreSingleton(),
            c => c.RichEntity(),
            c => c.RichTransient(),
            ScopedBySuffix,
            c => c.Unique(),
            c => c.UriReturnIsRedirect(),
            UseBuiltInTypes,
            c => c.UseNullableTypes(),
            c => c.ValueType()
        ]);

        app.Features.AddCommunication(Communication);
        app.Features.AddCore(Core);
        if (Cors is not null) { app.Features.AddCors(Cors); }
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
        app.Features.AddOrm(Orm);
        if (RateLimiter is not null) { app.Features.AddRateLimiter(RateLimiter); }

        if (MockOverrider is not null) { app.Features.AddMockOverrider(MockOverrider); }

        if (Theme is not null)
        {
            if (Mode == ExecutionMode.Run)
            {
                app.Layers.AddUi();
            }

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

            app.Features.AddTheme(Theme);
        }

        Configure(app);
    }
}