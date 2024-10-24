using Baked.Business;
using Baked.Domain.Model;
using Baked.RestApi.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.RichTransient;

public class InitializeUsingIdParameterConvention(DomainModel _domain)
    : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (!context.Controller.MappedType.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.Has<LocatableAttribute>()) { return; }
        if (context.Controller.MappedType.TryGetQueryType(_domain, out var _)) { return; }
        if (!context.Controller.MappedType.TryGetMembers(out var members)) { return; }
        if (context.Action.MappedMethod is null) { return; }
        if (context.Action.MappedMethod.Has<InitializerAttribute>()) { return; }

        var initializer = members.Methods.Having<InitializerAttribute>().Single();
        if (!initializer.DefaultOverload.Parameters.TryGetValue("id", out var parameter)) { return; }

        context.Action.Parameter["id"] =
            new(parameter.ParameterType, ParameterModelFrom.Route, parameter.Name, MappedParameter: parameter)
            {
                IsOptional = parameter.IsOptional,
                DefaultValue = parameter.DefaultValue,
                IsInvokeMethodParameter = false,
                RoutePosition = 1
            };

        var targetParameter = context.Action.Parameter["target"];
        targetParameter.Name = "newTarget";
        targetParameter.Type = $"Func<{targetParameter.Type}>";

        context.Action.RouteParts.RemoveAt(0);
        context.Action.RouteParts.Insert(0, context.Controller.MappedType.Name.Pluralize());
        context.Action.FindTargetStatement = $"newTarget().{initializer.Name}({initializer.DefaultOverload.Parameters.Select(p => $"@{p.Name}").Join(", ")})";
    }
}