using Baked.CodingStyle.InitializableViaMethodName;

namespace Baked;

public static class InitializableViaMethodNameCodingStyleExtensions
{
    extension(CodingStyleFeature.Configurator _)
    {
        public InitializableViaMethodNameCodingStyleFeature InitializableViaMethodName(
            string[]? initializerNames = default
        ) => new(initializerNames ?? ["With"]);
    }
}