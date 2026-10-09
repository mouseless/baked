using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Orm;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.RichEntity;

public class EntityInitializerIsPostResourceConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Type.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.Has<EntityAttribute>()) { return; }
        if (!context.Method.TryGet<ApiAction>(out var action)) { return; }
        if (!context.Method.Has<Initializer>()) { return; }

        action.Method = HttpMethod.Post;
        action.RouteParts = [context.Type.Name.Pluralize()];
    }
}