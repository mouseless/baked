using Do.RestApi.Configuration;
using System.Text.RegularExpressions;

namespace Do.RestApi.Conventions;

public class AutoHttpMethodConvention(IEnumerable<(Regex Regex, HttpMethod Method)> _mappings)
    : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        foreach (var mapping in _mappings)
        {
            if (!mapping.Regex.IsMatch(context.Action.Id)) { continue; }

            context.Action.Method = mapping.Method;

            return;
        }
    }
}