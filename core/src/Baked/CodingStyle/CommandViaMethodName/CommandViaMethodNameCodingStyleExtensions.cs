using Baked.CodingStyle;
using Baked.CodingStyle.CommandViaMethodName;

namespace Baked;

public static class CommandViaMethodNameCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public CommandViaMethodNameCodingStyleFeature CommandViaMethodName(
            IEnumerable<string>? methodNames = default
        ) => new(methodNames ?? ["Execute", "Process"]);
    }
}