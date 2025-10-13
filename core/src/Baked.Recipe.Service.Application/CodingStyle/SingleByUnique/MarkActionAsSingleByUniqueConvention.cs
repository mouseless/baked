using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.SingleByUnique;

public class MarkActionAsSingleByUniqueConvention : IDomainModelConvention<TypeModelContext>, IDomainModelConvention<MethodModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMembers(out var members)) { return; }
        if (!members.TryGet<ControllerModelAttribute>(out var controller)) { return; }
        if (!controller.Action.TryGetValue("SingleById", out var action)) { return; }

        action.AdditionalAttributes.Add($"{nameof(SingleByUniqueAttribute)}(\"Id\", typeof(Guid))");
    }

    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (!context.Method.TryGet<SingleByUniqueAttribute>(out var unique)) { return; }

        action.AdditionalAttributes.Add($"{nameof(SingleByUniqueAttribute)}(\"{unique.PropertyName}\", typeof({unique.PropertyType.FullName}))");
    }
}