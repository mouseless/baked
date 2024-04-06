using Do.Branding;
using Do.Testing;

namespace Do.Test;

public static class BannerExtensions
{
    public static IBanner ABanner(this Mocker _) =>
        new Mock<IBanner>().Object;

    public static void VerifyPrinted(this IBanner source) =>
        Mock.Get(source).Verify(b => b.Print());

    public static DoBanner ADoBanner(this Stubber _) =>
        new();
}