using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.AddRemoveChild;

public class OnlyEntityParameterIsInRouteForDeleteChildConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModelAttribute>(out var action)) { return; }
        if (action.Method != HttpMethod.Delete) { return; }
        if (action.Name == string.Empty) { return; }
        if (action.InvokedMethodParameters.Count() != 1) { return; }

        var onlyParameter = action.InvokedMethodParameters.Single();
        if (onlyParameter.ManuallyAdded) { return; }
        if (!context.Method.DefaultOverload.Parameters[onlyParameter.Id].ParameterType.TryGetEntityAttribute(out var _)) { return; }

        onlyParameter.From = ParameterModelFrom.Route;
        onlyParameter.RoutePosition = 3;
    }
}