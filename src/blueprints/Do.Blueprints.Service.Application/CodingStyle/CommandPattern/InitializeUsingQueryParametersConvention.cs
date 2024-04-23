using Do.Business;
using Do.Lifetime;
using Do.Orm;
using Do.RestApi.Configuration;
using Do.RestApi.Model;

namespace Do.CodingStyle.CommandPattern;

public class InitializeUsingQueryParametersConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (context.Action.MethodModel is null) { return; }
        if (!context.Controller.TypeModel.TryGetMembers(out var members)) { return; }
        if (!members.Has<TransientAttribute>()) { return; }
        if (members.Has<EntityAttribute>()) { return; } // get this from another way

        var initializer = members.Methods.Having<InitializerAttribute>().First(i => i.Overloads.Any(o => o.IsPublic && o.AllParametersAreApiInput()));
        var overload = initializer.Overloads.First(o => o.IsPublic && o.AllParametersAreApiInput()); // fix this public & all parameters duplication and reuse configured overload selector

        foreach (var parameter in overload.Parameters)
        {
            context.Action.Parameter[parameter.Name] =
                new(parameter.ParameterType, ParameterModelFrom.Query, parameter.Name)
                {
                    IsOptional = parameter.IsOptional,
                    DefaultValue = parameter.DefaultValue,
                    IsInvokeMethodParameter = false
                };
        }

        var targetParameter = context.Action.Parameter["target"];

        context.Action.FindTargetStatement = $"target.{initializer.Name}({overload.Parameters.Select(p => $"@{p.Name}").Join(", ")})";
    }
}