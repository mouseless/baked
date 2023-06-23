namespace Do.Test.Architecture.Branding;

public class PrintingBanner : Spec
{
    [Test]
    public void It_prints_banner_prior_to_build()
    {
        var mockBanner = MockMe.ABanner();
        var build = GiveMe.ABuild(banner: mockBanner.Object);

        build.As(_ => { });

        mockBanner.VerifyPrinted();
    }
}
