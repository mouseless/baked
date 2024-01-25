using Do.Test.RestApi.Analyzer;
using System.Reflection;

namespace Do.Test;

public abstract class TestServiceSpec : ServiceSpec
{
    static TestServiceSpec() =>
        Init(
            business: c => c.Default(options =>
            {
                options.AddBusinessAssembly<Entity>();
                options.AddApplicationPart<ParentsController>();
            }),
            configure: app =>
            {
                app.Features.AddConfigurationOverrider();
            }
        );

    protected override string? GetDefaultSettingsValue(string key) =>
        key == "Int" ? "42" : "test value";
}