using Baked.CodingStyle;
using Baked.CodingStyle.RecordsAreDtos;

namespace Baked;

public static class RecordsAreDtosCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public RecordsAreDtosCodingStyleFeature RecordsAreDtos() =>
            new();
    }
}