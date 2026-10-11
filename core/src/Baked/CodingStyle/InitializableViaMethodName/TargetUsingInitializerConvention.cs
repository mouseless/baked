using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.InitializableViaMethodName;

public class TargetUsingInitializerConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ApiAction>(out var action)) { return; }
        if (!context.Type.TryGetMembers(out var members)) { return; }
        if (members.Has<Locatable>()) { return; }
        if (!members.Methods.Having<Initializer>().Any()) { return; }
        if (context.Method.Has<Initializer>()) { return; }

        var initializer = members.Methods.Having<Initializer>().Single();
        var initializerParameters = action.Parameters.Where(p => initializer.DefaultOverload.Parameters.Contains(p.Id));
        action.FindTargetStatement = $"target.{initializer.Name}({initializerParameters.Select(p => $"{p.InternalName}: {p.RenderLookup($"@{p.Name}")}").Join(", ")})";
    }
}