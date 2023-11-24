namespace Do.Test;

public abstract class TestServiceSpec : ServiceSpec
{
    static TestServiceSpec() =>
        Init(
            business: c => c.Default(),
            configure: app =>
            {
                app.Features.AddConfigurationOverrider();
            }
        );

    protected override string? GetDefaultSettingsValue(string key) =>
        key == "Int" ? "42" : "test value";
}