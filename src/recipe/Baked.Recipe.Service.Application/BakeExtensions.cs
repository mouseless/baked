using Baked.Architecture;
using Baked.Authentication;
using Baked.Authorization;
using Baked.Business;
using Baked.Caching;
using Baked.Communication;
using Baked.Core;
using Baked.Cors;
using Baked.Database;
using Baked.ExceptionHandling;
using Baked.Greeting;
using Baked.Localization;
using Baked.Logging;
using Baked.Orm;
using Baked.RateLimiter;
using Baked.Reporting;
using Baked.Theme;

namespace Baked;

public static class BakeExtensions
{
    public static Application Service(this Bake bake,
        Func<BusinessConfigurator, IFeature<BusinessConfigurator>> business,
        IEnumerable<Func<AuthenticationConfigurator, IFeature<AuthenticationConfigurator>>>? authentications = default,
        Func<AuthorizationConfigurator, IFeature<AuthorizationConfigurator>>? authorization = default,
        IEnumerable<Func<CachingConfigurator, IFeature<CachingConfigurator>>>? cachings = default,
        Func<CommunicationConfigurator, IFeature<CommunicationConfigurator>>? communication = default,
        Func<CoreConfigurator, IFeature<CoreConfigurator>>? core = default,
        Func<CorsConfigurator, IFeature<CorsConfigurator>>? cors = default,
        Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>>? database = default,
        Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>>? exceptionHandling = default,
        Func<GreetingConfigurator, IFeature<GreetingConfigurator>>? greeting = default,
        Func<LocalizationConfigurator, IFeature<LocalizationConfigurator>>? localization = default,
        Func<LoggingConfigurator, IFeature<LoggingConfigurator>>? logging = default,
        Func<OrmConfigurator, IFeature<OrmConfigurator>>? orm = default,
        Func<RateLimiterConfigurator, IFeature<RateLimiterConfigurator>>? rateLimiter = default,
        Func<ThemeConfigurator, IFeature<ThemeConfigurator>>? theme = default,
        Action<ApplicationDescriptor>? configure = default
    )
    {
        authentications ??= [c => c.FixedBearerToken()];
        authorization ??= c => c.ClaimBased();
        cachings ??= [c => c.InMemory(), c => c.ScopedMemory()];
        communication ??= c => c.Http();
        core ??= c => c.Dotnet();
        cors ??= c => c.Disabled();
        database ??= c => c.Sqlite();
        exceptionHandling ??= c => c.ProblemDetails();
        greeting ??= c => c.Swagger();
        localization ??= c => c.Dotnet();
        logging ??= c => c.Request();
        orm ??= c => c.AutoMap();
        rateLimiter ??= c => c.Concurrency();
        configure ??= _ => { };

        return bake.Application(app =>
        {
            app.Layers.AddCodeGeneration();
            app.Layers.AddDataAccess();
            app.Layers.AddDomain();
            app.Layers.AddHttpClient();
            app.Layers.AddHttpServer();
            app.Layers.AddRestApi();
            app.Layers.AddRuntime();

            app.Features.AddAuthentications(authentications);
            app.Features.AddAuthorization(authorization);
            app.Features.AddBinding(c => c.Rest());
            app.Features.AddBusiness(business);
            app.Features.AddCachings(cachings);
            app.Features.AddCodingStyles(
            [
                c => c.AddRemoveChild(),
                c => c.CommandPattern(),
                c => c.EntityExtensionViaComposition(),
                c => c.EntitySubclassViaComposition(),
                c => c.NamespaceAsRoute(),
                c => c.ObjectAsJson(),
                c => c.RecordsAreDtos(),
                c => c.RemainingServicesAreSingleton(),
                c => c.RichEntity(),
                c => c.RichTransient(),
                c => c.ScopedBySuffix(),
                c => c.SingleByUnique(),
                c => c.UriReturnIsRedirect(),
                c => c.UseBuiltInTypes(),
                c => c.UseNullableTypes(),
                c => c.WithMethod()
            ]);
            app.Features.AddCommunication(communication);
            app.Features.AddCore(core);
            app.Features.AddCors(cors);
            app.Features.AddDatabase(database);
            app.Features.AddExceptionHandling(exceptionHandling);
            app.Features.AddGreeting(greeting);
            app.Features.AddLifetimes(
            [
                c => c.Scoped(),
                c => c.Singleton(),
                c => c.Transient()
            ]);
            app.Features.AddLocalization(localization);
            app.Features.AddLogging(logging);
            app.Features.AddOrm(orm);
            app.Features.AddRateLimiter(rateLimiter);

            if (theme is not null)
            {
                app.Layers.AddUi();

                app.Features.AddUx(
                [
                    c => c.ActionsAreGroupedAsTabs(),
                    c => c.ActionsAsDataPanels(),
                    c => c.DataTableVisualizesList(),
                    c => c.DataTableVisualizesObjectWithList(),
                    c => c.DesignatedStringPropertiesAreLabel(),
                    c => c.EnumParameterIsSelect(),
                    c => c.InitializerParametersAreInPageTitle(),
                    c => c.NumericValuesAreFormatted(),
                    c => c.PanelParametersAreStateful(),
                    c => c.TypeWithOnlyGetIsReportPage()
                ]);

                app.Features.AddTheme(theme);
            }

            configure(app);
        });
    }

    public static Application DataSource(this Bake bake,
        Func<BusinessConfigurator, IFeature<BusinessConfigurator>> business,
        IEnumerable<Func<CachingConfigurator, IFeature<CachingConfigurator>>>? cachings = default,
        Func<CoreConfigurator, IFeature<CoreConfigurator>>? core = default,
        Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>>? database = default,
        Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>>? exceptionHandling = default,
        Func<GreetingConfigurator, IFeature<GreetingConfigurator>>? greeting = default,
        Func<LocalizationConfigurator, IFeature<LocalizationConfigurator>>? localization = default,
        Func<LoggingConfigurator, IFeature<LoggingConfigurator>>? logging = default,
        Func<RateLimiterConfigurator, IFeature<RateLimiterConfigurator>>? rateLimiter = default,
        Func<ReportingConfigurator, IFeature<ReportingConfigurator>>? reporting = default,
        Action<ApplicationDescriptor>? configure = default
    )
    {
        cachings ??= [c => c.InMemory(), c => c.ScopedMemory()];
        core ??= c => c.Dotnet();
        database ??= c => c.Sqlite();
        exceptionHandling ??= c => c.ProblemDetails();
        greeting ??= c => c.Swagger();
        localization ??= c => c.Dotnet();
        logging ??= c => c.Request();
        rateLimiter ??= c => c.Concurrency();
        reporting ??= c => c.NativeSql();

        configure ??= _ => { };

        return bake.Application(app =>
        {
            app.Layers.AddCodeGeneration();
            app.Layers.AddDataAccess();
            app.Layers.AddDomain();
            app.Layers.AddHttpServer();
            app.Layers.AddRestApi();
            app.Layers.AddRuntime();

            app.Features.AddBinding(c => c.Rest());
            app.Features.AddBusiness(business);
            app.Features.AddCachings(cachings);
            app.Features.AddCodingStyles(
            [
                c => c.AddRemoveChild(),
                c => c.CommandPattern(),
                c => c.NamespaceAsRoute(),
                c => c.RecordsAreDtos(),
                c => c.RemainingServicesAreSingleton(),
                c => c.RichTransient(),
                c => c.ScopedBySuffix(),
                c => c.UseBuiltInTypes(),
                c => c.UseNullableTypes(),
                c => c.WithMethod()
            ]);
            app.Features.AddCore(core);
            app.Features.AddDatabase(database);
            app.Features.AddExceptionHandling(exceptionHandling);
            app.Features.AddGreeting(greeting);
            app.Features.AddLifetimes(
            [
                c => c.Scoped(),
                c => c.Singleton(),
                c => c.Transient()
            ]);
            app.Features.AddLocalization(localization);
            app.Features.AddLogging(logging);
            app.Features.AddRateLimiter(rateLimiter);
            app.Features.AddReporting(reporting);

            configure(app);
        });
    }
}