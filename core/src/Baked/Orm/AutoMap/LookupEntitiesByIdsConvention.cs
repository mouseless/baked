using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Orm.AutoMap;

public class LookupEntitiesByIdsConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (!context.Parameter.TryGet<ParameterModelAttribute>(out var parameter)) { return; }
        if (!parameter.IsInvokeMethodParameter) { return; }
        if (!context.Parameter.ParameterType.TryGetElementType(out var entityType)) { return; }
        if (!entityType.TryGetQueryContextType(context.Domain, out var queryContextType)) { return; }
        if (!entityType.TryGetIdInfo(out var idInfo)) { return; }

        var queryContextParameter = action.AddQueryContextAsService(queryContextType);

        parameter.ConvertToIds(idInfo);
        parameter.LookupRenderer = p => queryContextParameter.BuildByIds(p,
            isArray: context.Parameter.ParameterType.IsArray
        );
    }
}