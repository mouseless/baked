using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using System.Diagnostics.CodeAnalysis;

namespace Baked.Orm.AutoMap;

public class LookupEntityByIdConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Parameter.TryGet<ParameterModelAttribute>(out var parameter)) { return; }
        if (!context.Parameter.ParameterType.TryGetQueryContextType(context.Domain, out var queryContextType)) { return; }

        var notNull = context.Parameter.Has<NotNullAttribute>();

        ParameterModelAttribute? queryContextParameter = null;
        if (context.Method.TryGet<ActionModelAttribute>(out var action))
        {
            // parameter belongs to an action, add service to the parent action
            queryContextParameter = action.AddQueryContextAsService(queryContextType);
        }
        else if (context.Method.Has<InitializerAttribute>())
        {
            // parameter belongs to an initializer, add service to all actions
            foreach (var otherAction in context.Type.Methods.Having<ActionModelAttribute>().Select(m => m.Get<ActionModelAttribute>()))
            {
                queryContextParameter = otherAction.AddQueryContextAsService(queryContextType);
            }
        }

        if (queryContextParameter is null) { return; }

        if (!context.Parameter.ParameterType.TryGetIdInfo(out var idInfo)) { return; }

        parameter.ConvertToId(idInfo.Type, idInfo.Name, nullable: !notNull);

        parameter.LookupRenderer = p => queryContextParameter.BuildSingleBy(p, idInfo.Name,
            notNullValueExpression: $"({idInfo.Type}){p}",
            nullable: !notNull
        );
    }
}