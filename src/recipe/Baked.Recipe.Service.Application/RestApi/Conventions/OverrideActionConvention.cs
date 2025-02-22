using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.RestApi.Conventions;

public class OverrideActionConvention(Func<MethodModelContext, bool> _when, Action<ActionModel> _configuration)
    : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModel>(out var action)) { return; }
        if (!_when(context)) { return; }

        _configuration(action);
    }
}