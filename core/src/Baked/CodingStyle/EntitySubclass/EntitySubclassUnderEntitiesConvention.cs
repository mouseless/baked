using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.EntitySubclass;

public class EntitySubclassUnderEntitiesConvention : IDomainModelConvention<TypeModelContext>, IDomainModelConvention<MethodModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.TryGet<ControllerModelAttribute>(out var controller)) { return; }
        if (!context.Type.TryGetEntityTypeFromSubclass(context.Domain, out var entityType)) { return; }

        controller.GroupName = entityType.Name.Pluralize();
    }

    public void Apply(MethodModelContext context)
    {
        if (context.Method.Has<InitializerAttribute>()) { return; }
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (!context.Type.TryGetEntityTypeFromSubclass(context.Domain, out var entityType)) { return; }

        action.RouteParts = [entityType.Name.Pluralize(), action.Name];
    }
}