using Do.Domain.Model;
using Do.Test.Communication;
using Do.Test.Orm;

namespace Do.Test;

public abstract class TestServiceSpec : ServiceSpec
{
    protected static DomainModel DomainModel { get; private set; } = default!;

    static TestServiceSpec()
    {
        var context = Init(
            business: c => c.Default(assemblies: [typeof(Entity).Assembly]),
            communication: c => c.Mock(defaultResponses: response =>
            {
                response.ForClient<External>(response: "test result");
                response.ForClient<Remote>(response: "path1 response", when: r => r.UrlOrPath.Equals("path1"));
                response.ForClient<Remote>(response: "path2 response", when: r => r.UrlOrPath.Equals("path2"));
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
