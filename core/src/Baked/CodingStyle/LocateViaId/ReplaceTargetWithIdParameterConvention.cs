using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Lifetime;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.LocateViaId;

public class ReplaceTargetWithIdParameterConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Type.Has<Transient>()) { return; }
        if (!context.Method.TryGet<ApiAction>(out var action)) { return; }
        if (!context.Type.TryGetMembers(out var members)) { return; }
        if (!members.Has<Locatable>()) { return; }
        if (!members.TryGetIdInfo(out var idInfo)) { return; }
        if (context.Method.Has<Initializer>()) { return; }

        action.Parameter.Remove(ApiParameter.TargetParameterName);
        action.Parameter[idInfo.PropertyName.Camelize()] =
            new(idInfo.PropertyName.Camelize(), idInfo.Type, ParameterModelFrom.Route)
            {
                IsInvokeMethodParameter = false,
                RoutePosition = 1
            };
        action.Parameter[idInfo.PropertyName.Camelize()].AdditionalAttributes.Add($"SwaggerSchema(\"Unique value to find {context.Type.Name.Humanize().ToLowerInvariant()} resource\")");
    }
}