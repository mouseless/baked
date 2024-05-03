using Do.Architecture;
using Do.Authentication;
using Do.Business;
using Do.Caching;
using Do.Communication;
using Do.Core;
using Do.Database;
using Do.ExceptionHandling;
using Do.Greeting;
using Do.Logging;
using Do.Orm;
using Humanizer;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace Do.Testing;

public abstract class ServiceNfr<TEntryPoint> : Nfr
    where TEntryPoint : class, IEntryPoint
{
    protected System.Net.Http.HttpClient Client { get; private set; } = default!;

    protected virtual bool AllowAutoRedirect => false;

    public override async Task OneTimeSetUp()
    {
        await base.OneTimeSetUp();

        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentName);

        Client = new WebApplicationFactory<TEntryPoint>()
            .WithWebHostBuilder(config => config.UseSetting("typeName", $"{GetType().AssemblyQualifiedName}"))
            .CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = AllowAutoRedirect });
    }

    public override async Task OneTimeTearDown()
    {
        await base.OneTimeTearDown();

        foreach (var entityName in EntityNamesToClearOnTearDown)
        {
            var entitiesRoute = entityName.Kebaberize().Pluralize();
            var entitiesResponse = await Client.GetAsync($"/{entitiesRoute}");
            await CheckResponse($"GET /{entitiesRoute}", entitiesResponse);

            var entities = (IEnumerable?)JsonConvert.DeserializeObject(await entitiesResponse.Content.ReadAsStringAsync()) ?? Array.Empty<object>();
            foreach (dynamic entity in entities)
            {
                var deleteResponse = await Client.DeleteAsync($"/{entitiesRoute}/{entity?.id}");
                await CheckResponse($"DELETE /{entitiesRoute}/{entity?.id}", deleteResponse);
            }
        }
    }

    async Task CheckResponse(string route, HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode) { return; }

        throw new($"'{route}' didn't work as expected: {response.StatusCode}\n{await response.Content.ReadAsStringAsync()}");
    }

    protected override Application ForgeApplication() =>
        Forge.New
            .Service(
                authentications: Authentications,
                business: Business,
                caching: Caching,
                core: Core,
                communication: Communication,
                database: Database,
                exceptionHandling: ExceptionHandling,
                greeting: Greeting,
                logging: Logging,
                orm: Orm,
                configure: Configure
            );

    protected virtual IEnumerable<string> EntityNamesToClearOnTearDown => [];

    protected virtual IEnumerable<Func<AuthenticationConfigurator, IFeature<AuthenticationConfigurator>>>? Authentications => default;
    protected abstract Func<BusinessConfigurator, IFeature<BusinessConfigurator>> Business { get; }
    protected virtual Func<CachingConfigurator, IFeature<CachingConfigurator>>? Caching => default;
    protected virtual Func<CommunicationConfigurator, IFeature<CommunicationConfigurator>>? Communication => default;
    protected virtual Func<CoreConfigurator, IFeature<CoreConfigurator>>? Core => default;
    protected virtual Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>>? Database => default;
    protected virtual Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>>? ExceptionHandling => default;
    protected virtual Func<GreetingConfigurator, IFeature<GreetingConfigurator>>? Greeting => default;
    protected virtual Func<LoggingConfigurator, IFeature<LoggingConfigurator>>? Logging => default;
    protected virtual Func<OrmConfigurator, IFeature<OrmConfigurator>>? Orm => default;
    protected virtual Action<ApplicationDescriptor>? Configure => default;
}