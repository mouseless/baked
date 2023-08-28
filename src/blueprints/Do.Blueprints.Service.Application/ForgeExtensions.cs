using Do.Architecture;
using Do.Business;
using Do.Core;
using Do.Database;
using Do.Documentation;
using Do.ExceptionHandling;
using Do.Greeting;
using Do.Logging;
using Do.Orm;

namespace Do;

public static class ForgeExtensions
{
    public static Application Service(this Forge source,
        Func<BusinessConfigurator, IFeature<BusinessConfigurator>> business,
        Func<CoreConfigurator, IFeature<CoreConfigurator>>? core = default,
        Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>>? database = default,
        Func<DocumentationConfigurator, IFeature<DocumentationConfigurator>>? documentation = default,
        Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>>? exceptionHandling = default,
        Func<GreetingConfigurator, IFeature<GreetingConfigurator>>? greeting = default,
        Func<LoggingConfigurator, IFeature<LoggingConfigurator>>? logging = default,
        Func<OrmConfigurator, IFeature<OrmConfigurator>>? orm = default,
        Action<ApplicationDescriptor>? configure = default
    )
    {
        core ??= c => c.Dotnet();
        database ??= c => c.Sqlite();
        documentation ??= c => c.Default();
        exceptionHandling ??= c => c.Default();
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
                app.Features.AddDatabase(database);
                app.Features.AddDocumentation(documentation);
                app.Features.AddExceptionHandling(exceptionHandling);
                app.Features.AddGreeting(greeting);
                app.Features.AddLogging(logging);
                app.Features.AddOrm(orm);

                configure(app);
            });
    }
}
