using Baked.Playground.Communication;
using Baked.Playground.Orm;

namespace Baked.Test;

public abstract class TestSpec : MonolithSpec
{
    static TestSpec() =>
        Init(
            business: c => c.DomainAssemblies(typeof(Entity).Assembly, baseNamespace: "Baked.Playground"),
            communication: c => c.Mock(defaultResponses: response =>
            {
                response.ForClient<GitHubClient>(response: "test result");
                response.ForClient<InternalSamples>(response: "path1 response", when: r => r.UrlOrPath.Equals("path1"));
                response.ForClient<InternalSamples>(response: "path2 response", when: r => r.UrlOrPath.Equals("path2"));
            }),
            configure: app =>
            {
                app.Features.AddReporting(c => c.Mock());
                app.Features.AddOverrides();
            }
        );

    protected override string? GetDefaultSettingsValue(string key) =>
        key switch
        {
            _ when key.EndsWith("Minutes") => null,
            _ when key == "Int" => $"{GiveMe.AnInteger()}",
            _ => GiveMe.AString()
        };
}