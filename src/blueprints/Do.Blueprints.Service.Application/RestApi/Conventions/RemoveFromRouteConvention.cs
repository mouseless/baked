using Do.RestApi.Configuration;
using Humanizer;

namespace Do.RestApi.Conventions;

public class RemoveFromRouteConvention(IEnumerable<string> _parts,
    Func<ActionModelContext, bool>? _when = default,
    bool _pluralize = false
) : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (_when is not null && !_when(context)) { return; }

        var changed = false;
        foreach (var part in _parts.Where(p => context.Action.Route.Contains(p)))
        {
            context.Action.Route = context.Action.Route.Replace(part, string.Empty);
            context.Action.Name = context.Action.Name.Replace(part, string.Empty);
            changed = true;
        }

        if (_pluralize && changed && !string.IsNullOrEmpty(context.Action.Name))
        {
            var plural = context.Action.Name.Pluralize();
            context.Action.Route = context.Action.Route.Replace(context.Action.Name, plural);
            context.Action.Name = plural;
        }
    }
}