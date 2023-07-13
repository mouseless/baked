namespace Do.Test.Architecture.Branding;

public class PrintingBanner : Spec
{
    [Test]
    public void It_prints_banner_prior_to_build()
    {
        var banner = MockMe.ABanner();
        var forge = GiveMe.AForge(banner: banner);

        forge.Application(_ => { });

        banner.VerifyPrinted();
    }
}
