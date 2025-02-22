using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Orm.AutoMap;

public class LookupEntitiesByIdsConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModelAttribute>(out var action)) { return; }
        if (!context.Parameter.TryGetSingle<ParameterModelAttribute>(out var parameter)) { return; }
        if (!parameter.IsInvokeMethodParameter) { return; }
        if (!context.Parameter.ParameterType.TryGetElementType(out var entityType)) { return; }
        if (!entityType.TryGetQueryContextType(context.Domain, out var queryContextType)) { return; }

        var queryContextParameter = action.AddQueryContextAsService(queryContextType);

        parameter.ConvertToIds();
        parameter.LookupRenderer = p => queryContextParameter.BuildByIds(
            valueExpression: p,
            isArray: context.Parameter.ParameterType.IsArray
        );
    }
}