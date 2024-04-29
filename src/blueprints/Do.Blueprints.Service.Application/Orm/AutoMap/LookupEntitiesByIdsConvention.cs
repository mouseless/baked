using Do.Domain.Model;
using Do.RestApi.Configuration;

namespace Do.Orm.AutoMap;

public class LookupEntitiesByIdsConvention(DomainModel _domain)
    : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Parameter.IsInvokeMethodParameter) { return; }
        if (!context.Parameter.TypeModel.TryGetElementType(out var entityType)) { return; }
        if (!entityType.TryGetQueryContextType(_domain, out var queryContextType)) { return; }

        var queryContextParameter = context.Action.AddQueryContextAsService(queryContextType);

        context.Parameter.ConvertToIds();
        context.Parameter.LookupRenderer = p => queryContextParameter.BuildByIds(p, isArray: context.Parameter.TypeModel.IsArray);
    }
}