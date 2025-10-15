using Baked.Core;

namespace Baked.Test.Core;

public class TransformingText : TestSpec
{
    [Test]
    public void A_text_transformer_is_provided()
    {
        var service = GiveMe.The<ITextTransformer>();

        service.ShouldNotBeNull();
    }

    [Test]
    public void It_camelizes()
    {
        var service = GiveMe.The<ITextTransformer>();

        var actual = service.Camelize("sample text");

        actual.ShouldBe("sampleText");
    }

    [Test]
    public void It_kebaberizes()
    {
        var service = GiveMe.The<ITextTransformer>();

        var actual = service.Kebaberize("SampleText");

        actual.ShouldBe("sample-text");
    }

    [Test]
    public void It_pascalizes()
    {
        var service = GiveMe.The<ITextTransformer>();

        var actual = service.Pascalize("sample text");

        actual.ShouldBe("SampleText");
    }

    [Test]
    public void It_pluralizes()
    {
        var service = GiveMe.The<ITextTransformer>();

        var actual = service.Pluralize("sample text");

        actual.ShouldBe("sample texts");
    }

    [Test]
    public void It_singularizes()
    {
        var service = GiveMe.The<ITextTransformer>();

        var actual = service.Singularize("sample texts");

        actual.ShouldBe("sample text");
    }

    [Test]
    public void It_snakerizes()
    {
        var service = GiveMe.The<ITextTransformer>();

        var actual = service.Snakerize("SampleText");

        actual.ShouldBe("sample_text");
    }

    [Test]
    public void It_titleizes()
    {
        var service = GiveMe.The<ITextTransformer>();

        var actual = service.Titleize("SampleText");

        actual.ShouldBe("Sample Text");
    }
}