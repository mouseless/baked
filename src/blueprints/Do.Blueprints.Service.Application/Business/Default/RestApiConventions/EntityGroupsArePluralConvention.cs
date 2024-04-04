using Do.Orm;
using Do.RestApi.Configuration;
using Humanizer;

namespace Do.Business.Default.RestApiConventions;

public class EntityGroupsArePluralConvention : IApiModelConvention<ControllerModelContext>
{
    public void Apply(ControllerModelContext context)
    {
        if (!context.Controller.TypeModel.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.Has<EntityAttribute>()) { return; }

        context.Controller.GroupName = context.Controller.GroupName.Pluralize(inputIsKnownToBeSingular: true);
    }
}
