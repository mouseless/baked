using Do.Business;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Testing;

public abstract class ServiceNfr : Nfr
{
    protected static IServiceProvider ServiceProvider { get; private set; } = default!;
    protected static Func<WebApplicationFactoryClientOptions, System.Net.Http.HttpClient> CreateClient { get; private set; } = default!;

    protected static IConfiguration Configuration => ServiceProvider.GetRequiredService<IConfiguration>();

    protected static void Init<TEntryPoint>() where TEntryPoint : class
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", nameof(Nfr));

        var webApplicationFactory = new WebApplicationFactory<TEntryPoint>();
        ServiceProvider = webApplicationFactory.Services;
        CreateClient = webApplicationFactory.CreateClient;
    }

    protected System.Net.Http.HttpClient Client { get; private set; } = default!;
    protected virtual bool AllowAutoRedirect => false;

    public override async Task OneTimeSetUp()
    {
        await base.OneTimeSetUp();

        Caster.SetServiceProvider(ServiceProvider);
        Client = CreateClient(new() { AllowAutoRedirect = AllowAutoRedirect });
    }
}