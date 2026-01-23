using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.TransientBinding;

public class TargetUsingInitializerConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (!context.Type.TryGetMembers(out var members)) { return; }
        if (members.Has<LocatableAttribute>()) { return; }
        if (!members.Methods.Having<InitializerAttribute>().Any()) { return; }
        if (context.Method.Has<InitializerAttribute>()) { return; }

        var initializer = members.Methods.Having<InitializerAttribute>().Single();
        var initializerParameters = action.Parameters.Where(p => initializer.DefaultOverload.Parameters.Contains(p.Id));
        action.FindTargetStatement = $"target.{initializer.Name}({initializerParameters.Select(p => $"{p.InternalName}: {p.RenderLookup($"@{p.Name}")}").Join(", ")})";
        action.RouteParts = [context.Type.Name.Pluralize(), action.Name];
    }
}