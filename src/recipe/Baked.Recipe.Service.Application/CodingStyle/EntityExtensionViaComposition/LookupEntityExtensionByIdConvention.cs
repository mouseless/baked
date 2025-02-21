using Baked.Domain.Model;
using System.Diagnostics.CodeAnalysis;

namespace Baked.CodingStyle.EntityExtensionViaComposition;

public class LookupEntityExtensionByIdConvention(DomainModel _domain)
    : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (context.Parameter.IsTarget()) { return; }

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