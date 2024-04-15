using Do.RestApi.Configuration;

namespace Do.RestApi.Conventions;

public class RemoveActionNameFromRouteConvention(IEnumerable<string> actionNames)
    : IApiModelConvention<ActionModelContext>
{
    readonly HashSet<string> _actionNames = new(actionNames);

    public void Apply(ActionModelContext context)
    {
        if (!_actionNames.Contains(context.Action.Name)) { return; }

        context.Action.Route = context.Action.Route.Replace($"/{context.Action.Name}", string.Empty);
    }
}