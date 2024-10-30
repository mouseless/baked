using Baked.Business;
using Baked.Orm;
using Baked.RestApi.Configuration;
using Humanizer;

namespace Baked.CodingStyle.RichEntity;

public class EntityInitializerIsPostResourceConvention : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (context.Parameter.IsInvokeMethodParameter) { return; }
        if (!context.Parameter.TypeModel.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.Has<EntityAttribute>()) { return; }
        if (context.Action.MappedMethod is null) { return; }
        if (!context.Action.MappedMethod.Has<InitializerAttribute>()) { return; }

        context.Action.Method = HttpMethod.Post;
        context.Action.RouteParts = [context.Parameter.TypeModel.Name.Pluralize()];
    }
}