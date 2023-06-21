using Do.Branding;

namespace Do.Test;

public static class ArchitectureStubberExtensions
{
    public static Build ABuild(this Spec.Stubber source,
        IBanner? banner = default
    )
    {
        banner ??= new Mock<IBanner>().Object;

        return new(banner);
    }
}
