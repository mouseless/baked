using Do.RestApi.Configuration;

namespace Do.Business.Default.RestApiConventions;

public class RemoveActionNameFromRouteConvention(params string[] actionIds)
    : IApiModelConvention<ActionModelContext>
{
    readonly HashSet<string> _actionIds = new(actionIds);

    public void Apply(ActionModelContext context)
    {
        if (!_actionIds.Contains(context.Action.Id)) { return; }

        context.Action.Route = context.Action.Route.Replace($"/{context.Action.Id}", string.Empty);
    }
}