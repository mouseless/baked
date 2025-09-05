using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.RichTransient;

public class RichTransientInitializerIsGetResourceConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (!context.Type.TryGetMembers(out var members)) { return; }
        if (!members.Has<LocatableAttribute>()) { return; }
        if (!members.Properties.Any(p => p.IsPublic)) { return; }
        if (!context.Method.Has<InitializerAttribute>()) { return; }
        if (!action.Parameter.TryGetValue("id", out var parameter)) { return; }

        parameter.From = ParameterModelFrom.Route;
        parameter.RoutePosition = 1;
        parameter.IsInvokeMethodParameter = true;

        action.Method = HttpMethod.Get;
        action.RouteParts = [context.Type.Name.Pluralize()];
    }
}