using Do.Business;
using Do.Domain.Model;
using Do.RestApi.Configuration;

namespace Do.Orm.AutoMap;

public class LookupEntityByIdConvention(DomainModel _domain)
    : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (context.Action.MethodModel?.Has<InitializerAttribute>() == true) { return; }
        if (!context.Parameter.IsInvokeMethodParameter) { return; }
        if (!context.Parameter.TypeModel.TryGetQueryContextType(_domain, out var queryContextType)) { return; }

        var queryContextParameter = context.Action.AddQueryContextAsService(queryContextType);

        context.Parameter.ConvertToId();
        context.Parameter.LookupRenderer = p => queryContextParameter.BuildSingleBy(p);
    }
}