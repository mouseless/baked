using Baked.Ux;
using Baked.Ux.DesignatedStringPropertiesAreLabel;

namespace Baked;

public static class DesignatedStringPropertiesAreLabelUxExtensions
{
    public static DesignatedStringPropertiesAreLabelUxFeature DesignatedStringPropertiesAreLabel(this UxConfigurator _,
        IEnumerable<string>? propertyNames = default
    )
    {
        propertyNames ??= ["Display", "Label", "Name", "Title"];

        return new(propertyNames);
    }
}