using Do.Architecture;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Do.Test;

public abstract class IntegrationSpec<T> : IIntegrationSpec
    where T : IntegrationSpec<T>
{
    static WebApplicationFactory<IntegrationTestProgram> _factory = default!;

    static IntegrationSpec()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");

        _factory = new WebApplicationFactory<IntegrationTestProgram>().WithWebHostBuilder(a =>
        {
            a.UseSetting("type", $"{typeof(T).FullName}");
        });
    }

    internal WebApplicationFactory<IntegrationTestProgram> Factory => _factory;
    protected abstract Application Application { get; }

    void IIntegrationSpec.Run() => Application.Run();
}
