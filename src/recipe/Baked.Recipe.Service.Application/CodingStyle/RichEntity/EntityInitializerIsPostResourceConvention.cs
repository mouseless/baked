using Baked.Business;
using Baked.Orm;
using Baked.RestApi.Configuration;
using Humanizer;

namespace Baked.CodingStyle.RichEntity;

public class EntityInitializerIsPostResourceConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Controller.MappedType is null) { return; }
        if (!context.Controller.MappedType.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.Has<EntityAttribute>()) { return; }
        if (context.Action.MappedMethod is null) { return; }
        if (!context.Action.MappedMethod.Has<InitializerAttribute>()) { return; }

        context.Action.Method = HttpMethod.Post;
        context.Action.RouteParts = [context.Controller.MappedType.Name.Pluralize()];
    }
}