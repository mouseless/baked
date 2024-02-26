using Do.Architecture;
using Do.Authentication;
using Do.Business;
using Do.Caching;
using Do.Communication;
using Do.Core;
using Do.Database;
using Do.Documentation;
using Do.ExceptionHandling;
using Do.Greeting;
using Do.Logging;
using Do.Orm;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Do.Testing;

public abstract class ServiceNfr<TEntryPoint> : Nfr
    where TEntryPoint : class, IEntryPoint
{
    protected System.Net.Http.HttpClient Client { get; private set; } = default!;

    public override void OneTimeSetUp()
    {
        base.OneTimeSetUp();

        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentName);

        Client = new WebApplicationFactory<TEntryPoint>()
            .WithWebHostBuilder(config => config.UseSetting("typeName", $"{GetType().AssemblyQualifiedName}"))
            .CreateClient();
    }

    protected override Application ForgeApplication() =>
        Forge.New
            .Service(
                authentication: Authentication,
                business: Business,
                caching: Caching,
                core: Core,
                communication: Communication,
                database: Database,
                documentation: Documentation,
                exceptionHandling: ExceptionHandling,
                greeting: Greeting,
                logging: Logging,
                orm: Orm,
                configure: Configure
            );

    protected virtual Func<AuthenticationConfigurator, IFeature<AuthenticationConfigurator>>? Authentication => default;
    protected abstract Func<BusinessConfigurator, IFeature<BusinessConfigurator>> Business { get; }
    protected virtual Func<CachingConfigurator, IFeature<CachingConfigurator>>? Caching => default;
    protected virtual Func<CommunicationConfigurator, IFeature<CommunicationConfigurator>>? Communication => default;
    protected virtual Func<CoreConfigurator, IFeature<CoreConfigurator>>? Core => default;
    protected virtual Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>>? Database => c => c.InMemory();
    protected virtual Func<DocumentationConfigurator, IFeature<DocumentationConfigurator>>? Documentation => default;
    protected virtual Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>>? ExceptionHandling => default;
    protected virtual Func<GreetingConfigurator, IFeature<GreetingConfigurator>>? Greeting => default;
    protected virtual Func<LoggingConfigurator, IFeature<LoggingConfigurator>>? Logging => default;
    protected virtual Func<OrmConfigurator, IFeature<OrmConfigurator>>? Orm => default;
    protected virtual Action<ApplicationDescriptor>? Configure => default;
}
