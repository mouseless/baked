using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.EntityExtensionViaComposition;

public class LookupEntityExtensionsByIdsConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (!context.Parameter.TryGet<ParameterModelAttribute>(out var parameter)) { return; }
        if (!parameter.IsInvokeMethodParameter) { return; }
        if (!context.Parameter.ParameterType.TryGetElementType(out var entityExtensionType)) { return; }
        if (!entityExtensionType.TryGetEntityTypeFromExtension(context.Domain, out var entityType)) { return; }
        if (!entityType.TryGetQueryContextType(context.Domain, out var queryContextType)) { return; }

        var queryContextParameter = action.AddQueryContextAsService(queryContextType);

        if (!entityType.TryGetIdInfo(out var idInfo)) { return; }

        parameter.ConvertToIds(idInfo);
        parameter.LookupRenderer = p => queryContextParameter.BuildByIds(p, castTo: entityExtensionType, isArray: context.Parameter.ParameterType.IsArray);
    }
}