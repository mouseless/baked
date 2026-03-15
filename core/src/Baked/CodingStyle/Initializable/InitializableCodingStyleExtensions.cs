using Baked.CodingStyle;
using Baked.CodingStyle.Initializable;

namespace Baked;

public static class InitializableCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public InitializableCodingStyleFeature Initializable(
            string[]? initializerNames = default
        ) => new(initializerNames ?? ["With"]);
    }
}