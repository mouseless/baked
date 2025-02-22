using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using System.Diagnostics.CodeAnalysis;

namespace Baked.CodingStyle.EntityExtensionViaComposition;

public class LookupEntityExtensionByIdConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModel>(out var action)) { return; }
        if (!context.Parameter.TryGetSingle<ParameterModel>(out var parameter)) { return; }
        if (parameter.IsTarget()) { return; }

        var entityExtensionType = context.Parameter.ParameterType;
        if (!entityExtensionType.TryGetEntityTypeFromExtension(context.Domain, out var entityType)) { return; }
        if (!entityType.TryGetQueryContextType(context.Domain, out var queryContextType)) { return; }

        var notNull = context.Parameter.Has<NotNullAttribute>();
        var queryContextParameter = action.AddQueryContextAsService(queryContextType);

        parameter.ConvertToId(nullable: !notNull);
        parameter.LookupRenderer =
            p => queryContextParameter.BuildSingleBy(p,
                    notNullValueExpression: $"(Guid){p}",
                    castTo: entityExtensionType,
                    nullable: !notNull
                );
    }
}