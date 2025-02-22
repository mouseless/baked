using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.EntitySubclassViaComposition;

public class EntitySubclassInitializerIsPostResourceConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Parameter.TryGetSingle<ParameterModel>(out var parameter)) { return; }
        if (parameter.IsInvokeMethodParameter) { return; }
        if (!context.Parameter.ParameterType.TryGetSubclassName(out var subclassName)) { return; }
        if (!context.Parameter.ParameterType.TryGetEntityTypeFromSubclass(context.Domain, out var entityType)) { return; }
        if (!context.Method.TryGetSingle<ActionModel>(out var action)) { return; }
        if (action.ManuallyAdded) { return; }
        if (!context.Method.Has<InitializerAttribute>()) { return; }

        action.RouteParts = [entityType.Name.Pluralize(), subclassName];
    }
}