using Baked.Business;
using Baked.Domain.Model;
using Baked.RestApi.Configuration;
using Humanizer;

namespace Baked.CodingStyle.EntitySubclassViaComposition;

public class EntitySubclassInitializerIsPostResourceConvention(DomainModel _domain)
    : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (context.Parameter.IsInvokeMethodParameter) { return; }
        if (!context.Parameter.TypeModel.TryGetSubclassName(out var subclassName)) { return; }
        if (!context.Parameter.TypeModel.TryGetEntityTypeFromSubclass(_domain, out var entityType)) { return; }
        if (context.Action.MappedMethod is null) { return; }
        if (!context.Action.MappedMethod.Has<InitializerAttribute>()) { return; }

        context.Action.RouteParts = [entityType.Name.Pluralize(), subclassName];
    }
}