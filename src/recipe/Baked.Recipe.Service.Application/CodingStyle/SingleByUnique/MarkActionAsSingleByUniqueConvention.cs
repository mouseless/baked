using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.SingleByUnique;

public class MarkActionAsSingleByUniqueConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModelAttribute>(out var action)) { return; }
        if (action.Id == "SingleById")
        {
            action.AdditionalAttributes.Add($"{nameof(SingleByUniqueAttribute)}(\"Id\", typeof(Guid))");

            return;
        }

        if (context.Method is null) { return; }
        if (context.Method.TryGetSingle<SingleByUniqueAttribute>(out var unique))
        {
            action.AdditionalAttributes.Add($"{nameof(SingleByUniqueAttribute)}(\"{unique.PropertyName}\", typeof({unique.PropertyType.FullName}))");
        }
    }
}