using Baked.RestApi.Configuration;
using Baked.RestApi.Model;

namespace Baked.RestApi.Conventions;

public class OverrideActionConvention(Func<ActionModelContext, bool> _when, Action<ActionModel> _configuration) :
    IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (!_when(context)) { return; }

        _configuration(context.Action);
    }
}