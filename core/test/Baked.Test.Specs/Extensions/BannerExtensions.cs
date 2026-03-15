using Baked.Architecture;
using Baked.Branding;
using Baked.Testing;

namespace Baked.Test;

public static class BannerExtensions
{
    extension(Mocker _)
    {
        public IBanner ABanner() =>
            new Mock<IBanner>().Object;
    }

    extension(IBanner banner)
    {
        public void VerifyPrinted() =>
            Mock.Get(banner).Verify(b => b.Print());
    }

    extension(Stubber _)
    {
        public BakedBanner ABakedBanner(
            RunFlags runFlags = RunFlags.Start
        ) => new(runFlags);
    }
}