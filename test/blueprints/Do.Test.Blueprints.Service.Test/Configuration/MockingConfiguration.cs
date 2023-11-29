using Microsoft.Extensions.Configuration;

namespace Do.Test.Configuration;

public class MockingConfiguration : TestServiceSpec
{
    [Test]
    public void Mock_configuration_returns_mocked_settings_value()
    {
        MockMe.ASetting("Config", "10");
        var configuration = GiveMe.The<IConfiguration>();

        var actual = configuration.GetRequiredValue<int>("Config");

        actual.ShouldBe(10);
    }

    [TestCase(42)]
    [TestCase("value")]
    [TestCase(false)]
    public void Mock_ASetting_value_parameter_is_generic<T>(T value)
    {
        MockMe.ASetting("Config", value);
        var configuration = GiveMe.The<IConfiguration>();

        var actual = configuration.GetRequiredValue<T>("Config");

        actual.ShouldBeEquivalentTo(value);
    }

    [TestCase("Int", 42)] // defined in TestServiceSpec
    [TestCase("String", "test value")] // defined in TestServiceSpec
    public void Mock_configuration_uses_settings_value_provider_for_not_mocked_config_sections(string key, object value)
    {
        var configuration = GiveMe.The<IConfiguration>();

        var actual = configuration.GetRequiredValue(value.GetType(), key);

        actual.ShouldBe(value);
    }
}
