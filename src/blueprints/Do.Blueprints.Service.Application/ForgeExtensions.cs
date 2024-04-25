using Do.Architecture;
using Do.Authentication;
using Do.Authorization;
using Do.Business;
using Do.Caching;
using Do.Communication;
using Do.Core;
using Do.Database;
using Do.ExceptionHandling;
using Do.Greeting;
using Do.Logging;
using Do.Orm;

namespace Do;

public static class ForgeExtensions
{
    public static Application Service(this Forge source,
        Func<BusinessConfigurator, IFeature<BusinessConfigurator>> business,
        IEnumerable<Func<AuthenticationConfigurator, IFeature<AuthenticationConfigurator>>>? authentications = default,
        Func<AuthorizationConfigurator, IFeature<AuthorizationConfigurator>>? authorization = default,
        Func<CachingConfigurator, IFeature<CachingConfigurator>>? caching = default,
        Func<CommunicationConfigurator, IFeature<CommunicationConfigurator>>? communication = default,
        Func<CoreConfigurator, IFeature<CoreConfigurator>>? core = default,
        Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>>? database = default,
        Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>>? exceptionHandling = default,
        Func<GreetingConfigurator, IFeature<GreetingConfigurator>>? greeting = default,
        Func<LoggingConfigurator, IFeature<LoggingConfigurator>>? logging = default,
        Func<OrmConfigurator, IFeature<OrmConfigurator>>? orm = default,
        Action<ApplicationDescriptor>? configure = default
    )
    {
        authentications ??= [c => c.FixedBearerToken()];
        authorization ??= c => c.ClaimBased(claims: ["User"]);
        caching ??= c => c.ScopedMemory();
        communication ??= c => c.Http();
        core ??= c => c.Dotnet();
        database ??= c => c.Sqlite();
        exceptionHandling ??= c => c.Default();
        greeting ??= c => c.Swagger();
        logging ??= c => c.Request();
        orm ??= c => c.AutoMap();
        configure ??= _ => { };

        return source.Application(app =>
        {
            app.Layers.AddCodeGeneration();
            app.Layers.AddConfiguration();
            app.Layers.AddDataAccess();
            app.Layers.AddDependencyInjection();
            app.Layers.AddDomain();
            app.Layers.AddHttpClient();
            app.Layers.AddHttpServer();
            app.Layers.AddMonitoring();
            app.Layers.AddRestApi();

            app.Features.AddAuthentications(authentications);
            app.Features.AddAuthorization(authorization);
            app.Features.AddBusiness(business);
            app.Features.AddCaching(caching);
            app.Features.AddCodingStyles([
                c => c.WithMethod(),
                c => c.ScopedBySuffix(),
                c => c.RemainingServicesAreSingleton(),
                c => c.UseBuiltInTypes(),
                c => c.ObjectAsJson(),
                c => c.RichEntity()
            ]);
            app.Features.AddCommunication(communication);
            app.Features.AddCore(core);
            app.Features.AddDatabase(database);
            app.Features.AddExceptionHandling(exceptionHandling);
            app.Features.AddGreeting(greeting);
            app.Features.AddLifetimes([
                c => c.Singleton(),
                c => c.Scoped(),
                c => c.Transient()
            ]);
            app.Features.AddLogging(logging);
            app.Features.AddOrm(orm);

            configure(app);
        });
    }
}