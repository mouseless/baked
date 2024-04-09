using Do.RestApi.Configuration;

namespace Do.RestApi.Conventions;

public class RemovePrefixFromRouteConvention(IEnumerable<string> _prefixes)
    : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (!_prefixes.Any(prefix => context.Action.Id.StartsWith(prefix))) { return; }

        context.Action.Route = context.Action.Route.Replace(context.Action.Id, context.Action.Id[3..]);
    }
}