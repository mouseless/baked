using Baked.Architecture;
using Baked.Branding;
using Baked.Testing;

namespace Baked.Test;

public static class BakeExtensions
{
    extension(Stubber giveMe)
    {
        public Bake ABake(
            IBanner? banner = default,
            ApplicationContext? startContext = default,
            ApplicationContext? generateContext = default,
            RunFlags runflags = RunFlags.Start
        )
        {
            banner ??= giveMe.Spec.MockMe.ABanner();
            startContext ??= new();

            return new(banner, () => new(startContext, generateContext), runflags);
        }

        public DiagnosticsCode ADiagnosticsCode() =>
            new(999);
    }
}