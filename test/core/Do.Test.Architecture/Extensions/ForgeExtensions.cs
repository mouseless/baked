using Do.Architecture;
using Do.Branding;
using Do.Testing;

namespace Do.Test;

public static class ForgeExtensions
{
    public static Forge AForge(this Stubber giveMe,
        IBanner? banner = default,
        ApplicationContext? context = default
    )
    {
        banner ??= giveMe.Spec.MockMe.ABanner();
        context ??= new();

        return new(banner, () => new(context));
    }
}