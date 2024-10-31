using Baked.Business;
using Baked.RestApi.Configuration;

namespace Baked.CodingStyle.RichTransient;

public class FindTargetUsingInitializerConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (!context.Controller.MappedType.TryGetMembers(out var members)) { return; }
        if (!members.Methods.Having<InitializerAttribute>().Any()) { return; }
        if (context.Action.MappedMethod is null) { return; }
        if (context.Action.MappedMethod.Has<InitializerAttribute>()) { return; }

        var initializer = members.Methods.Having<InitializerAttribute>().Single();
        var initilzerParameters = context.Action.Parameters.Where(p => initializer.DefaultOverload.Parameters.Contains(p.Id));
        context.Action.FindTargetStatement = $"target.{initializer.Name}({initilzerParameters.Select(p => $"{p.InternalName}: {p.RenderLookup($"@{p.Name}")}").Join(", ")})";
    }
}