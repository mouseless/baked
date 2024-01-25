using System.Reflection;

namespace Do.Test;

public abstract class TestServiceSpec : ServiceSpec
{
    static TestServiceSpec() =>
        Init(
            business: c => c.Default(businessAssemblies: [typeof(Entity).Assembly], applicationParts: [Assembly.GetExecutingAssembly()]),
            configure: app =>
            {
                app.Features.AddConfigurationOverrider();
            }
        );

    protected override string? GetDefaultSettingsValue(string key) =>
        key == "Int" ? "42" : "test value";
}