namespace Do.Test;

public abstract class TestServiceSpec : ServiceSpec
{
    static TestServiceSpec() =>
        Init(
            business: c => c.Default(assemblies: [typeof(Entity).Assembly]),
            communication: c => c.Mock(options =>
            {
                options.DefaultResponse = new { value = "default response" };
                options.AddClient<Singleton>([new(r => true, "test result")]);
                options.AddClient<Operation>([
                    new(r => r.UrlOrPath.Equals("path1"), "path1 response"),
                    new(r => r.UrlOrPath.Equals("path2"), "path2 response")
                ]);
            }),
            configure: app =>
            {
                app.Features.AddConfigurationOverrider();
            }
        );

    protected override string? GetDefaultSettingsValue(string key) =>
        key == "Int" ? "42" : "test value";
}