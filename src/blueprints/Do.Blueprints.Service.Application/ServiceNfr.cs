using Microsoft.AspNetCore.Mvc.Testing;

namespace Do.Testing;

public abstract class ServiceNfr<TEntryPoint> : Nfr
    where TEntryPoint : class, IEntryPoint
{
    protected HttpClient Client { get; private set; } = default!;

    public override void OneTimeSetUp()
    {
        base.OneTimeSetUp();

        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentName);

        Client = new WebApplicationFactory<TEntryPoint>()
            .WithWebHostBuilder(config => config.UseSetting("typeName", $"{GetType().AssemblyQualifiedName}"))
            .CreateClient();
    }
}
