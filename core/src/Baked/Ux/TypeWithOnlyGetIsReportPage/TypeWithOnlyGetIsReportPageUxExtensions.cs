using Baked.Ux;
using Baked.Ux.TypeWithOnlyGetIsReportPage;

namespace Baked;

public static class TypeWithOnlyGetIsReportPageUxExtensions
{
    public static TypeWithOnlyGetIsReportPageUxFeature TypeWithOnlyGetIsReportPage(this UxConfigurator _) =>
        new();
}