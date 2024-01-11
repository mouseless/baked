using Microsoft.AspNetCore.Mvc.Testing;

namespace Do.Test;

public abstract class IntegrationSpec<T> where T : IntegrationSpec<T>
{
    static WebApplicationFactory<IntegrationTestProgram> _factory = default!;

    static IntegrationSpec()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");

        _factory = new WebApplicationFactory<IntegrationTestProgram>().WithWebHostBuilder(a =>
        {
            a.UseSetting("type", $"{typeof(T).FullName}");
            a.UseSetting("method", nameof(Run));
        });
    }

    internal WebApplicationFactory<IntegrationTestProgram> Factory => _factory;

    public abstract void Run();
}
