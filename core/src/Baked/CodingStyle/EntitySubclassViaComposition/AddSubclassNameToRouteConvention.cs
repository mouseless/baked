using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.EntitySubclassViaComposition;

public class AddSubclassNameToRouteConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        var entitySubclassType = context.Type;
        if (!entitySubclassType.TryGetSubclassName(out var subclassName)) { return; }
        if (!entitySubclassType.TryGetEntityTypeFromSubclass(context.Domain, out var entityType)) { return; }
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }

        action.RouteParts = [entityType.Name.Pluralize(), subclassName, action.Name];
    }
}