using Baked.Domain.Model;
using Baked.Test.Communication;
using Baked.Test.Orm;

namespace Baked.Test;

public abstract class TestServiceSpec : ServiceSpec
{
    protected static DomainModel DomainModel { get; private set; } = default!;

    static TestServiceSpec()
    {
        var context = Init(
            business: c => c.DomainAssemblies([typeof(Entity).Assembly]),
            communication: c => c.Mock(defaultResponses: response =>
            {
                response.ForClient<ExternalSamples>(response: "test result");
                response.ForClient<InternalSamples>(response: "path1 response", when: r => r.UrlOrPath.Equals("path1"));
                response.ForClient<InternalSamples>(response: "path2 response", when: r => r.UrlOrPath.Equals("path2"));
            }),
            configure: app =>
            {
                app.Features.AddConfigurationOverrider();
            }
        );

        DomainModel = context.GetDomainModel();
    }

    protected override string? GetDefaultSettingsValue(string key) =>
        key == "Int" ? $"{GiveMe.AnInteger()}" : GiveMe.AString();
}