using Baked.CodingStyle;
using Baked.CodingStyle.InitializableViaMethodName;

namespace Baked;

public static class InitializableViaMethodNameCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public InitializableViaMethodNameCodingStyleFeature InitializableViaMethodName(
            string[]? initializerNames = default
        ) => new(initializerNames ?? ["With"]);
    }
}