using Microsoft.Extensions.Configuration;

namespace Do.Test.Configuration;

public class MockingConfiguration : BlueprintsServiceSpec
{
    protected override Func<string, string?> SettingsValueProvider => GetSettingsValue;

    string? GetSettingsValue(string key) => key.Equals("Int") ? "42" : "test value";

    [Test]
    public void Mock_configuration_returns_mocked_settings_value()
    {
        MockMe.ASetting("Config", "10");
        var configuration = GiveMe.The<IConfiguration>();

        var actual = configuration.GetRequiredValue<int>("Config");

        actual.ShouldBe(10);
    }

    [TestCase("Int", 42)]
    [TestCase("String", "test value")]
    public void Mock_configuration_uses_settings_value_provider_for_not_mocked_config_sections(string key, object value)
    {
        MockMe.ASetting("Config", "10");
        var configuration = GiveMe.The<IConfiguration>();

        var actual = configuration.GetRequiredValue(value.GetType(), key);

        actual.ShouldBe(value);
    }
}
