using Do.Architecture;

namespace Do.Test;

public abstract class TestServiceSpec : ServiceSpec
{
    static ApplicationContext _applicationContext = default!;

    protected ApplicationContext ApplicationContext => _applicationContext;

    static TestServiceSpec() =>
        _applicationContext = Init(
            configure: app =>
            {
                app.Features.AddConfigurationOverrider();
            }
        );

    protected override string? GetDefaultSettingsValue(string key) =>
        key == "Int" ? "42" : "test value";
}