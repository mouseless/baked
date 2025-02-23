using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.RestApi.Conventions;

public class ProducesJsonConvention(
    Func<ActionModelAttribute, bool>? _when = default
) : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModelAttribute>(out var action)) { return; }
        if (_when is not null && !_when(action)) { return; }

        action.AdditionalAttributes.Add("Produces(\"application/json\")");
    }
}