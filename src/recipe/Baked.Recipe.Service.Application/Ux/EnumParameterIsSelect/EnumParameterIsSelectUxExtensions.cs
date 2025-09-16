using Baked.Ux;
using Baked.Ux.EnumParameterIsSelect;

namespace Baked;

public static class EnumParameterIsSelectUxExtensions
{
    public static EnumParameterIsSelectUxFeature EnumParameterIsSelect(this UxConfigurator _,
        int maxMemberCountForSelectButton = 3
    ) => new(maxMemberCountForSelectButton);
}