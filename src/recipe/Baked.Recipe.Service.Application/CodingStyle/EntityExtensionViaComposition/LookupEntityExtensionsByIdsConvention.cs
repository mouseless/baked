using Baked.Domain.Model;
using Baked.RestApi.Configuration;

namespace Baked.CodingStyle.EntityExtensionViaComposition;

public class LookupEntityExtensionsByIdsConvention(DomainModel _domain)
    : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Parameter.IsInvokeMethodParameter) { return; }
        if (!context.Parameter.TypeModel.TryGetElementType(out var entityExtensionType)) { return; }
        if (!entityExtensionType.TryGetEntityTypeFromExtension(_domain, out var entityType)) { return; }
        if (!entityType.TryGetQueryContextType(_domain, out var queryContextType)) { return; }

        var queryContextParameter = context.Action.AddQueryContextAsService(queryContextType);

        context.Parameter.ConvertToIds();
        context.Parameter.LookupRenderer = p => queryContextParameter.BuildByIds(p, castTo: entityExtensionType, isArray: context.Parameter.TypeModel.IsArray);
    }
}