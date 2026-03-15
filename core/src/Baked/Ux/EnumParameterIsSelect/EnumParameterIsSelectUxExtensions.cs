using Baked.Ux;
using Baked.Ux.EnumParameterIsSelect;

namespace Baked;

public static class EnumParameterIsSelectUxExtensions
{
    extension(UxConfigurator _)
    {
        public EnumParameterIsSelectUxFeature EnumParameterIsSelect(
            int maxMemberCountForSelectButton = 3
        ) => new(maxMemberCountForSelectButton);
    }
}