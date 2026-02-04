using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Lifetime;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.Initializable;

public class RemoveInitializerNameFromRouteConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (!context.Type.Has<TransientAttribute>()) { return; }
        if (!context.Method.Has<InitializerAttribute>()) { return; }

        var initializerPart = action.RouteParts.FirstOrDefault(p => p == context.Method.Name);
        if (initializerPart is not null)
        {
            action.RouteParts.Remove(initializerPart);
        }
    }
}