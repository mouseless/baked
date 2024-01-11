using Do.Architecture;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Do.Test;

public abstract class IntegrationSpec
{
    static WebApplicationFactory<TestProgram> _factory = default!;

    static IntegrationSpec()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");

        _factory = new WebApplicationFactory<TestProgram>();
    }

    public static void Run() =>
        Forge.New
            .Service(
               business: c => c.Default(),
               database: c => c.Sqlite(),
               exceptionHandling: ex => ex.Default(typeUrlFormat: "https://do.mouseless.codes/errors/{0}"),
               configure: app => app.Features.AddConfigurationOverrider()
           )
        .Run();

    public WebApplicationFactory<TestProgram> Factory => _factory;
}

public class TestProgram
{
    public static void Main(string[] args)
    {
        IntegrationSpec.Run();
    }
}
