using Do.Orm;
using Do.RestApi.Configuration;
using Humanizer;

namespace Do.CodingStyle.RichEntity;

public class EntityUnderEntitiesConvention : IApiModelConvention<ControllerModelContext>
{
    public void Apply(ControllerModelContext context)
    {
        if (!context.Controller.MappedType.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.Has<EntityAttribute>()) { return; }

        context.Controller.GroupName = context.Controller.GroupName.Pluralize(inputIsKnownToBeSingular: true);
    }
}