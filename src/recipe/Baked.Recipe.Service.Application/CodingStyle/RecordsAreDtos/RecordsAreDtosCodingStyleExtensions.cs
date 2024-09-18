using Baked.CodingStyle;
using Baked.CodingStyle.RecordsAreDtos;

namespace Baked;

public static class RecordsAreDtosCodingStyleExtensions
{
    public static RecordsAreDtosCodingStyleFeature RecordsAreDtos(this CodingStyleConfigurator _) =>
        new();
}