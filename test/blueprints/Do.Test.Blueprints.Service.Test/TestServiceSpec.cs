namespace Do.Test;

public abstract class TestServiceSpec : ServiceSpec
{
    static TestServiceSpec() =>
        Init(
            business: c => c.Default(assemblies: [typeof(Entity).Assembly]),
            communication: c => c.Mock(defaultResponses =>
            {
                defaultResponses.ForClient<Singleton>(setup: new("test result"));
                defaultResponses.ForClient<Operation>(
                    new("path1 response", When: r => r.UrlOrPath.Equals("path1")),
                    new("path2 response", When: r => r.UrlOrPath.Equals("path2"))
                );
            }),
            configure: app =>
            {
                app.Features.AddConfigurationOverrider();
            }
        );

    protected override string? GetDefaultSettingsValue(string key) =>
        key == "Int" ? "42" : "test value";
}