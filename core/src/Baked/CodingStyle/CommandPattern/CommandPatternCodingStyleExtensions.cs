using Baked.CodingStyle;
using Baked.CodingStyle.CommandPattern;

namespace Baked;

public static class CommandPatternCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public CommandPatternCodingStyleFeature CommandPattern(
            IEnumerable<string>? methodNames = default
        ) => new(methodNames ?? ["Execute", "Process"]);
    }
}