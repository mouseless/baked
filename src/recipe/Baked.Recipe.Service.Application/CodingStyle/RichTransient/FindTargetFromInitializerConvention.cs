using Baked.Business;
using Baked.RestApi.Configuration;

namespace Baked.CodingStyle.RichTransient;

public class FindTargetFromInitializerConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (!context.Controller.MappedType.TryGetMembers(out var members)) { return; }
        if (!members.Methods.Having<InitializerAttribute>().Any()) { return; }
        if (context.Action.MappedMethod is null) { return; }
        if (context.Action.MappedMethod.Has<InitializerAttribute>()) { return; }

        var targetParameter = context.Action.Parameter["target"];
        targetParameter.Name = "newTarget";
        targetParameter.Type = $"Func<{targetParameter.Type}>";

        var initializer = members.Methods.Having<InitializerAttribute>().Single();
        context.Action.FindTargetStatement = $"newTarget().{initializer.Name}({initializer.DefaultOverload.Parameters.Select(p => $"@{p.Name}").Join(", ")})";
    }
}