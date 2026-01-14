using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using System.Diagnostics.CodeAnalysis;

namespace Baked.CodingStyle.EntityExtensionViaComposition;

public class LookupEntityExtensionByIdConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (!context.Parameter.TryGet<ParameterModelAttribute>(out var parameter)) { return; }

        var entityExtensionType = context.Parameter.ParameterType;
        if (!entityExtensionType.TryGetEntityTypeFromExtension(context.Domain, out var entityType)) { return; }
        if (!entityType.TryGetQueryContextType(context.Domain, out var queryContextType)) { return; }

        var notNull = context.Parameter.Has<NotNullAttribute>();
        var queryContextParameter = action.AddQueryContextAsService(queryContextType);

        var idAttribute = entityType.GetMembers().FirstProperty<IdAttribute>().Get<IdAttribute>();
        parameter.ConvertToId(idAttribute.Type, idAttribute.Key, nullable: !notNull);

        parameter.LookupRenderer =
            p => queryContextParameter.BuildSingleBy(p, idAttribute.Key,
                    notNullValueExpression: $"({idAttribute.Type}){p}",
                    castTo: entityExtensionType,
                    nullable: !notNull
                );
    }
}