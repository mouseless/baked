using Baked.Architecture;
using Baked.Branding;
using Baked.Testing;

namespace Baked.Test;

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