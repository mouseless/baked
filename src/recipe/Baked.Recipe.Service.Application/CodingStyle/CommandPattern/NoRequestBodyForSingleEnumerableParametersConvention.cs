namespace Baked.CodingStyle.CommandPattern;

public class NoRequestBodyForSingleEnumerableParametersConvention(
    Func<ActionModelContext, bool>? _when = default,
    HttpMethod? _method = default
) : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (_when is not null && !_when(context)) { return; }
        if (context.Action.InvokedMethodParameters.Count() != 1) { return; }

        var onlyParameter = context.Action.InvokedMethodParameters.Single();
        if (onlyParameter.MappedParameter is null) { return; }
        if (!onlyParameter.MappedParameter.ParameterType.IsAssignableTo<IEnumerable>()) { return; }

        context.Action.UseRequestClassForBody = false;

        if (_method is not null) { context.Action.Method = _method; }
    }
}