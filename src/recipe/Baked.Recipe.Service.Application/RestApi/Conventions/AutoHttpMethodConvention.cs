using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using System.Text.RegularExpressions;

namespace Baked.RestApi.Conventions;

public class AutoHttpMethodConvention(IEnumerable<(Regex Regex, HttpMethod Method)> _mappings)
    : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModel>(out var action)) { return; }

        foreach (var mapping in _mappings)
        {
            if (!mapping.Regex.IsMatch(action.Name)) { continue; }

            action.Method = mapping.Method;

            return;
        }
    }
}