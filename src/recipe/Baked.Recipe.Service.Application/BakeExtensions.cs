using Baked.Architecture;
using Baked.Authentication;
using Baked.Authorization;
using Baked.Business;
using Baked.Caching;
using Baked.Communication;
using Baked.Core;
using Baked.Database;
using Baked.ExceptionHandling;
using Baked.Greeting;
using Baked.Logging;
using Baked.Orm;

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
        database ??= c => c.Sqlite();
        exceptionHandling ??= c => c.Default();
        greeting ??= c => c.Swagger();
        logging ??= c => c.Request();
        orm ??= c => c.AutoMap();
        configure ??= _ => { };

        return bake.Application(app =>
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
                c => c.AddRemoveChild(),
                c => c.CommandPattern(),
                c => c.EntityExtensionViaComposition(),
                c => c.EntitySubclassViaComposition(),
                c => c.ObjectAsJson(),
                c => c.RemainingServicesAreSingleton(),
                c => c.RichEntity(),
                c => c.ScopedBySuffix(),
                c => c.SingleByUnique(),
                c => c.UriReturnIsRedirect(),
                c => c.UseBuiltInTypes(),
                c => c.UseNullableTypes(),
                c => c.WithMethod()
            ]);
            app.Features.AddCommunication(communication);
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
            app.Features.AddOrm(orm);

            configure(app);
        });
    }
}