namespace Do.Test;

public abstract class TestServiceSpec : ServiceSpec
{
    static TestServiceSpec() =>
        Init(
            business: c => c.Default(assemblies: [typeof(Entity).Assembly]),
            communication: c => c.Mock(defaultResponses =>
            {
                defaultResponses.ForClient<Singleton>("test result");
                defaultResponses.ForClient<Operation>("path1 response", when: r => r.UrlOrPath.Equals("path1"));
                defaultResponses.ForClient<Operation>("path2 response", when: r => r.UrlOrPath.Equals("path2"));
            }),
            configure: app =>
            {
                app.Features.AddConfigurationOverrider();
            }
        );

    protected override string? GetDefaultSettingsValue(string key) =>
        key == "Int" ? "42" : "test value";
}