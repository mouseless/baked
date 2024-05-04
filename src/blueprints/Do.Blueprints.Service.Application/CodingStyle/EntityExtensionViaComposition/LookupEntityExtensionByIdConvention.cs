using Do.Business;
using Do.Domain.Model;
using Do.RestApi.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Do.CodingStyle.EntityExtensionViaComposition;

public class LookupEntityExtensionByIdConvention(DomainModel _domain)
    : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (context.Action.MappedMethod is null) { return; }
        if (context.Action.MappedMethod.Has<InitializerAttribute>()) { return; }
        if (!context.Parameter.IsInvokeMethodParameter) { return; }

        var entityExtensionType = context.Parameter.TypeModel;
        if (!entityExtensionType.TryGetEntityTypeFromExtension(_domain, out var entityType)) { return; }
        if (!entityType.TryGetQueryContextType(_domain, out var queryContextType)) { return; }

        var notNull = context.Parameter.MappedParameter?.Has<NotNullAttribute>() == true;
        var queryContextParameter = context.Action.AddQueryContextAsService(queryContextType);

        context.Parameter.ConvertToId(nullable: !notNull);
        context.Parameter.LookupRenderer =
            p => queryContextParameter.BuildSingleBy(p,
                    notNullValueExpression: $"(Guid){p}",
                    castTo: entityExtensionType,
                    nullable: !notNull
                );
    }
}