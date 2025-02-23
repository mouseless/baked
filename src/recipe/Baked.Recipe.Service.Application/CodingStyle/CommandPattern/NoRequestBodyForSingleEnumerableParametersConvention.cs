using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.CommandPattern;

public class NoRequestBodyForSingleEnumerableParametersConvention(
    Func<ActionModelAttribute, bool>? _when = default,
    HttpMethod? _method = default
) : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModelAttribute>(out var action)) { return; }
        if (_when is not null && !_when(action)) { return; }
        if (action.InvokedMethodParameters.Count() != 1) { return; }

        var onlyParameter = action.InvokedMethodParameters.Single();
        if (onlyParameter.Orphan) { return; }
        if (!context.Method.DefaultOverload.Parameters[onlyParameter.Id].ParameterType.IsAssignableTo<IEnumerable>()) { return; }

        action.UseRequestClassForBody = false;

        if (_method is not null) { action.Method = _method; }
    }
}