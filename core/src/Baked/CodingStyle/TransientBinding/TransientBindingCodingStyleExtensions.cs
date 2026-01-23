using Baked.CodingStyle;
using Baked.CodingStyle.TransientBinding;

namespace Baked;

public static class TransientBindingCodingStyleExtensions
{
    public static TransientBindingCodingStyleFeature TransientBinding(this CodingStyleConfigurator _) =>
        new();
}