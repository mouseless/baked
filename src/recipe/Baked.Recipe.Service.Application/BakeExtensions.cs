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
using Baked.Logging;
using Baked.Orm;
using Baked.Reporting;

namespace Baked;

public static class BakeExtensions
{
    public static Application Service(this Bake bake,
        Func<BusinessConfigurator, IFeature<BusinessConfigurator>> business,
        IEnumerable<Func<AuthenticationConfigurator, IFeature<AuthenticationConfigurator>>>? authentications = default,
        Func<AuthorizationConfigurator, IFeature<AuthorizationConfigurator>>? authorization = default,
        Func<CachingConfigurator, IFeature<CachingConfigurator>>? caching = default,
        Func<CommunicationConfigurator, IFeature<CommunicationConfigurator>>? communication = default,
        Func<CoreConfigurator, IFeature<CoreConfigurator>>? core = default,
        Func<CorsConfigurator, IFeature<CorsConfigurator>>? cors = default,
        Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>>? database = default,
        Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>>? exceptionHandling = default,
        Func<GreetingConfigurator, IFeature<GreetingConfigurator>>? greeting = default,
        Func<LoggingConfigurator, IFeature<LoggingConfigurator>>? logging = default,
        Func<OrmConfigurator, IFeature<OrmConfigurator>>? orm = default,
        Action<ApplicationDescriptor>? configure = default
    )
    {
        authentications ??= [c => c.FixedBearerToken()];
        authorization ??= c => c.ClaimBased();
        caching ??= c => c.ScopedMemory();
        communication ??= c => c.Http();
        core ??= c => c.Dotnet();
        cors ??= c => c.Disabled();
        database ??= c => c.Sqlite();
        exceptionHandling ??= c => c.ProblemDetails();
        greeting ??= c => c.Swagger();
        logging ??= c => c.Request();
        orm ??= c => c.AutoMap();
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
            app.Layers.AddUI();

            app.Features.AddAuthentications(authentications);
            app.Features.AddAuthorization(authorization);
            app.Features.AddBusiness(business);
            app.Features.AddCaching(caching);
            app.Features.AddCodingStyles([
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
            app.Features.AddLifetimes([
                c => c.Scoped(),
                c => c.Singleton(),
                c => c.Transient()
            ]);
            app.Features.AddLogging(logging);
            app.Features.AddOrm(orm);

            configure(app);
        });
    }

    public static Application DataSource(this Bake bake,
        Func<BusinessConfigurator, IFeature<BusinessConfigurator>> business,
        Func<CachingConfigurator, IFeature<CachingConfigurator>>? caching = default,
        Func<CoreConfigurator, IFeature<CoreConfigurator>>? core = default,
        Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>>? database = default,
        Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>>? exceptionHandling = default,
        Func<GreetingConfigurator, IFeature<GreetingConfigurator>>? greeting = default,
        Func<LoggingConfigurator, IFeature<LoggingConfigurator>>? logging = default,
        Func<ReportingConfigurator, IFeature<ReportingConfigurator>>? reporting = default,
        Action<ApplicationDescriptor>? configure = default
    )
    {
        caching ??= c => c.ScopedMemory();
        core ??= c => c.Dotnet();
        database ??= c => c.Sqlite();
        exceptionHandling ??= c => c.ProblemDetails();
        greeting ??= c => c.Swagger();
        logging ??= c => c.Request();
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

            app.Features.AddBusiness(business);
            app.Features.AddCaching(caching);
            app.Features.AddCodingStyles([
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
            app.Features.AddLifetimes([
                c => c.Scoped(),
                c => c.Singleton(),
                c => c.Transient()
            ]);
            app.Features.AddLogging(logging);
            app.Features.AddReporting(reporting);

            configure(app);
        });
    }
}