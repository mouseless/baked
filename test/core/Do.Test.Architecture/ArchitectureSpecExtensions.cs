using Do.Architecture;
using Do.Branding;

namespace Do.Test;

public static class ArchitectureSpecExtensions
{
    public static Build ABuild(this Spec.Stubber source,
        IBanner? banner = default
    )
    {
        banner ??= source.Spec.MockMe.ABanner().Object;

        return new(banner);
    }

    public static Mock<IBanner> ABanner(this Spec.Mocker source) => new();
    public static void VerifyPrinted(this Mock<IBanner> source) => source.Verify(b => b.Print());

    public static Mock<ILayer> ALayer(this Spec.Mocker source) => new();
    public static void VerifyInitialized(this Mock<ILayer> source) => source.Verify(l => l.Initialize(It.IsAny<object>()));
}
