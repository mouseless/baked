using Do.Architecture;
using Do.Business;
using Do.Core;
using Do.Database;
using Do.Greeting;
using Do.Logging;
using Do.Orm;

namespace Do;

public static class ForgeExtensions
{
    public static Application Service(this Forge source,
        Func<BusinessConfigurator, IFeature> business,
        Func<DatabaseConfigurator, IFeature> database,
        Func<CoreConfigurator, IFeature>? core = default,
        Func<DatabaseConfigurator, IFeature>? developmentDatabase = default,
        Func<GreetingConfigurator, IFeature>? greeting = default,
        Func<LoggingConfigurator, IFeature>? logging = default,
        Func<OrmConfigurator, IFeature>? orm = default,
        Action<ApplicationDescriptor>? configure = default
    )
    {
        core ??= c => c.Dotnet();
        greeting ??= c => c.Swagger();
        logging ??= c => c.RequestLogging();
        orm ??= c => c.Default();
        configure ??= _ => { };

        return source.Application(app =>
            {
                app.Layers.AddConfiguration();
                app.Layers.AddDataAccess();
                app.Layers.AddDependencyInjection();
                app.Layers.AddHttpServer();
                app.Layers.AddMonitoring();
                app.Layers.AddRestApi();

                app.Features.AddBusiness(business);
                app.Features.AddCore(core);
                app.Features.AddDatabase(database, developmentDatabase);
                app.Features.AddGreeting(greeting);
                app.Features.AddLogging(logging);
                app.Features.AddOrm(orm);

                configure(app);
            });
    }
}
