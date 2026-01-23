using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Lifetime;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.TransientBinding;

public class AddIdParameterToRouteConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Type.Has<TransientAttribute>()) { return; }
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (!context.Type.TryGetMembers(out var members)) { return; }
        if (!members.Has<LocatableAttribute>()) { return; }
        if (context.Method.Has<InitializerAttribute>()) { return; }

        var initializer = members.Methods.Having<InitializerAttribute>().Single();
        if (!initializer.DefaultOverload.Parameters.TryGetValue("id", out var parameter)) { return; }

        action.Parameter[ParameterModelAttribute.TargetParameterName] =
            new("id", parameter.ParameterType.CSharpFriendlyFullName, ParameterModelFrom.Route)
            {
                IsOptional = parameter.IsOptional,
                DefaultValue = parameter.DefaultValue,
                IsInvokeMethodParameter = false,
                RoutePosition = 1
            };
        action.Parameter[ParameterModelAttribute.TargetParameterName].AdditionalAttributes.Add($"SwaggerSchema(\"Unique value to find {context.Type.Name.Humanize().ToLowerInvariant()} resource\")");
    }
}