using Do.Business;
using Do.RestApi.Configuration;
using Do.RestApi.Model;

namespace Do.CodingStyle.CommandPattern;

public class InitializeUsingQueryParametersConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (!context.Controller.TypeModel.TryGetMembers(out var members)) { return; }
        if (!members.Has<PubliclyInitializableAttribute>()) { return; }

        var initializer = members.Methods.Having<InitializerAttribute>().Single();
        foreach (var parameter in initializer.DefaultOverload.Parameters)
        {
            context.Action.Parameter[parameter.Name] =
                new(parameter.ParameterType, ParameterModelFrom.Query, parameter.Name, MappedParameter: parameter)
                {
                    IsOptional = parameter.IsOptional,
                    DefaultValue = parameter.DefaultValue,
                    IsInvokeMethodParameter = false
                };
        }

        var targetParameter = context.Action.Parameter["target"];
        targetParameter.Name = "newTarget";
        targetParameter.Type = $"Func<{targetParameter.Type}>";

        context.Action.FindTargetStatement = $"newTarget().{initializer.Name}({initializer.DefaultOverload.Parameters.Select(p => $"@{p.Name}").Join(", ")})";
    }
}