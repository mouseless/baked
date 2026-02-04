using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.EntitySubclass;

public class EntitySubclassInitializerIsPostResourceConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.Has<InitializerAttribute>()) { return; }
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (!context.Type.TryGetSubclassName(out var subclassName)) { return; }
        if (!context.Type.TryGetEntityTypeFromSubclass(context.Domain, out var entityType)) { return; }

        action.RouteParts = [entityType.Name.Pluralize(), subclassName];
    }
}