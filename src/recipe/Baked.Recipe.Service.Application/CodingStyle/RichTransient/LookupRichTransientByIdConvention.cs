using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using System.Diagnostics.CodeAnalysis;

namespace Baked.CodingStyle.RichTransient;

public class LookupRichTransientByIdConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModelAttribute>(out var action)) { return; }
        if (!context.Parameter.TryGetSingle<ParameterModelAttribute>(out var parameter)) { return; }
        if (!context.Parameter.ParameterType.TryGetMembers(out var members)) { return; }
        if (!members.Has<LocatableAttribute>()) { return; }

        var initializer = members.Methods.Having<InitializerAttribute>().Single();
        if (!initializer.DefaultOverload.Parameters.TryGetValue("id", out var idParameter)) { return; }

        var notNull = context.Parameter.Has<NotNullAttribute>();
        var factoryParameter = action.AddFactoryAsService(context.Parameter.ParameterType);

        parameter.Type = $"{idParameter.ParameterType.CSharpFriendlyFullName}";
        parameter.Name = $"{context.Parameter.Name}Id";
        parameter.LookupRenderer = p => factoryParameter.BuildInitializerById(context.Parameter.ParameterType,
            valueExpression: p,
            nullable: !notNull
        );
    }
}