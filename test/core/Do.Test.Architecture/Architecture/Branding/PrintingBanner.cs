using Do.Branding;

namespace Do.Test.Architecture.Branding;

public class PrintingBanner : ArchitectureSpec
{
    [Test]
    public void It_prints_banner_prior_to_build()
    {
        var banner = MockMe.ABanner();
        var forge = GiveMe.AForge(banner: banner);

        forge.Application(_ => { });

        banner.VerifyPrinted();
    }

    [Test]
    public void Prints_the_correct_banner()
    {
        var writer = new StringWriter();
        Console.SetOut(writer);
        var banner = GiveMe.ADoBanner();

        banner.Print();

        var outputLines = writer.ToString().Split(Environment.NewLine);

        outputLines[0].ShouldBe("----------------------------------------------");
        outputLines[1].ShouldBe(string.Empty);
        outputLines[2].ShouldBe("  ██        ████████    ██████████");
        outputLines[3].ShouldBe("    ██      ██      ██  ██      ██");
        outputLines[4].ShouldBe("      ██    ██      ██  ██      ██");
        outputLines[5].ShouldBe("    ██      ██      ██  ██      ██");
        outputLines[6].ShouldBe("  ██        ████████    ██████████  ██████████");
    }
}
