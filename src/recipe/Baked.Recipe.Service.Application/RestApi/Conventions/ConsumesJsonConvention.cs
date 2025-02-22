using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.RestApi.Conventions;

public class ConsumesJsonConvention(
    Func<ActionModel, bool>? _when = default
) : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModel>(out var action)) { return; }
        if (_when is not null && !_when(action)) { return; }

        action.AdditionalAttributes.Add("Consumes(\"application/json\")");
    }
}