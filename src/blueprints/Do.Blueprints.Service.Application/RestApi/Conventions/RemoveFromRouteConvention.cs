using Do.RestApi.Configuration;

namespace Do.RestApi.Conventions;

public class RemoveFromRouteConvention(IEnumerable<string> _parts,
    Func<ActionModelContext, bool>? _when = default
) : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (_when is not null && !_when(context)) { return; }

        for (var i = 0; i < context.Action.RouteParts.Count; i++)
        {
            var routePart = context.Action.RouteParts[i];
            foreach (var part in _parts.Where(p => routePart.Contains(p)))
            {
                routePart = routePart.Replace(part, string.Empty);
            }

            context.Action.RouteParts[i] = routePart;
            if (string.IsNullOrWhiteSpace(routePart))
            {
                context.Action.RouteParts.RemoveAt(i);
                i--;
            }
        }

        foreach (var part in _parts)
        {
            context.Action.Name = context.Action.Name.Replace(part, string.Empty);
        }
    }
}