using Microsoft.Extensions.Localization;

namespace Baked.Test.Localization;

public class LocalizationDuringSpecs : TestSpec
{
    [Test]
    public void Localizer_is_avaliable_during_specs()
    {
        var l = GiveMe.The<IStringLocalizer>();

        var action = () => l[GiveMe.AString()];

        action.ShouldNotThrow();
    }

    [Test]
    public void It_returns_the_given_key()
    {
        var localizationSamples = GiveMe.The<LocalizationSamples>();

        string message = localizationSamples.GetLocaleString();

        message.ShouldBe("test");
    }

    [Test]
    public void It_supports_formatting()
    {
        var localizationSamples = GiveMe.The<LocalizationSamples>();

        string message = localizationSamples.GetParameterized("test");

        message.ShouldBe("Parameter value is 'test'");
    }
}