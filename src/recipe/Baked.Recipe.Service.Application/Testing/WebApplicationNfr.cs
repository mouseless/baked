using Baked.Business;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Testing;

// <note>
// belongs to `Baked.Http` package
// </note>
public abstract class WebApplicationNfr : Nfr
{
    protected static IServiceProvider ServiceProvider { get; private set; } = default!;
    protected static Func<WebApplicationFactoryClientOptions, System.Net.Http.HttpClient> CreateClient { get; private set; } = default!;

    protected static IConfiguration Configuration => ServiceProvider.GetRequiredService<IConfiguration>();

    protected static new void Init<TEntryPoint>() where TEntryPoint : class
    {
        Nfr.Init<TEntryPoint>();

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

    public override void SetUp()
    {
        base.SetUp();

        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", nameof(Nfr));
    }

    public override void TearDown()
    {
        base.TearDown();

        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", string.Empty);
    }
}