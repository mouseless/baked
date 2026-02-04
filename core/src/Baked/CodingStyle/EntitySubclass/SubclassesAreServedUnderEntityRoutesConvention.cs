using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.EntitySubclass;

// TODO find a better name or merge with 'EntitySubclassUnderEntitiesConvention'
public class SubclassesAreServedUnderEntityRoutesConvention
    : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (context.Method.Has<InitializerAttribute>()) { return; }
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (!context.Type.TryGetEntityTypeFromSubclass(context.Domain, out var entityType)) { return; }

        action.RouteParts = [entityType.Name.Pluralize(), action.Name];
    }
}