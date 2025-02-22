using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using System.Diagnostics.CodeAnalysis;

namespace Baked.Orm.AutoMap;

public class LookupEntityByIdConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModelAttribute>(out var action)) { return; }
        if (!context.Parameter.TryGetSingle<ParameterModelAttribute>(out var parameter)) { return; }
        if (parameter.IsTarget()) { return; }
        if (!context.Parameter.ParameterType.TryGetQueryContextType(context.Domain, out var queryContextType)) { return; }

        var notNull = context.Parameter.Has<NotNullAttribute>();
        var queryContextParameter = action.AddQueryContextAsService(queryContextType);

        parameter.ConvertToId(nullable: !notNull);
        parameter.LookupRenderer =
            p => queryContextParameter.BuildSingleBy(p,
                notNullValueExpression: $"(Guid){p}",
                nullable: !notNull
            );
    }
}