using Humanizer;

namespace Baked.RestApi.Conventions;

public class PluralizeActionConvention(
    Func<ActionModelContext, bool>? _when = default
) : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (_when is not null && !_when(context)) { return; }

        var newName = context.Action.Name.Pluralize();
        context.Action.RouteParts = context.Action.RouteParts.Replace(context.Action.Name, newName);
        context.Action.Name = newName;
    }
}