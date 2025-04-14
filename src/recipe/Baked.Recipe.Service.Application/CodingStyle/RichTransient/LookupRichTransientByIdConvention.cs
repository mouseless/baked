using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using System.Diagnostics.CodeAnalysis;

namespace Baked.CodingStyle.RichTransient;

public class LookupRichTransientByIdConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Parameter.TryGetSingle<ParameterModelAttribute>(out var parameter)) { return; }
        if (!context.Parameter.ParameterType.TryGetMembers(out var members)) { return; }
        if (!members.Has<LocatableAttribute>()) { return; }

        var initializer = members.Methods.Having<InitializerAttribute>().Single();
        if (!initializer.DefaultOverload.Parameters.TryGetValue("id", out var idParameter)) { return; }

        var actionShouldBeAsync = initializer.DefaultOverload.ReturnType.IsAssignableTo<Task>();

        if (context.Method.TryGetSingle<ActionModelAttribute>(out var action))
        {
            // parameter belongs to an action, add service to the parent action
            action.AddFactoryAsService(context.Parameter.ParameterType);

            if (actionShouldBeAsync)
            {
                action.MakeAsync();
            }
        }
        else if (context.Method.Has<InitializerAttribute>())
        {
            // parameter belongs to an initializer, add service to all actions
            foreach (var otherAction in context.Type.Methods.Having<ActionModelAttribute>().Select(m => m.GetSingle<ActionModelAttribute>()))
            {
                otherAction.AddFactoryAsService(context.Parameter.ParameterType);

                if (actionShouldBeAsync)
                {
                    otherAction.MakeAsync();
                }
            }
        }

        var notNull = context.Parameter.Has<NotNullAttribute>();

        parameter.Type = $"{idParameter.ParameterType.CSharpFriendlyFullName}";
        parameter.Name = $"{context.Parameter.Name}Id";
        parameter.LookupRenderer = p => context.Parameter.ParameterType.BuildInitializerById(p,
            nullable: !notNull
        );
    }
}