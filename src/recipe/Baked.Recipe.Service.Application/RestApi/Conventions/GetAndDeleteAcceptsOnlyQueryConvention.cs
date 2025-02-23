using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.RestApi.Conventions;

public class GetAndDeleteAcceptsOnlyQueryConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModelAttribute>(out var action)) { return; }
        if (!context.Parameter.TryGetSingle<ParameterModelAttribute>(out var parameter)) { return; }
        if (action.Method != HttpMethod.Get && action.Method != HttpMethod.Delete) { return; }
        if (parameter.FromServices || parameter.FromRoute) { return; }

        parameter.From = ParameterModelFrom.Query;
    }
}