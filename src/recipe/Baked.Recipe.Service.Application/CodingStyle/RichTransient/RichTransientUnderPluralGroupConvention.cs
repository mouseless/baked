using Baked.Business;
using Baked.RestApi.Configuration;
using Humanizer;

namespace Baked.CodingStyle.RichTransient;

public class RichTransientUnderPluralGroupConvention : IApiModelConvention<ControllerModelContext>
{
    public void Apply(ControllerModelContext context)
    {
        if (!context.Controller.MappedType.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.Has<LocatableAttribute>()) { return; }

        context.Controller.GroupName = context.Controller.GroupName.Pluralize();
    }
}