using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using System.Diagnostics.CodeAnalysis;

namespace Baked.CodingStyle.LocateViaId;

public class LookupLocatableParameterConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Parameter.ParameterType.TryGetMembers(out var parameterTypeMembers)) { return; }
        if (!context.Parameter.TryGet<ApiParameter>(out var parameter)) { return; }
        if (!parameterTypeMembers.TryGetIdInfo(out var idInfo)) { return; }
        if (!parameterTypeMembers.TryGet<Locatable>(out var locatable)) { return; }

        // NOTE action seems to have parameters set from somewhere even though the
        // default overload has zero parameters
        if (!context.Method.DefaultOverload.Parameters.Any()) { return; }

        ApiParameter? locatorServiceParameter = null;
        if (context.Method.TryGet<ApiAction>(out var action))
        {
            if (parameter.FromBodyOrForm && !action.UseForm) { return; }

            // parameter belongs to an action, add service to the parent action
            locatorServiceParameter = locatable.AddLocatorAsService(action, context.Parameter.ParameterType);
            if (locatable.IsAsync)
            {
                action.MakeAsync();
            }
        }
        else if (context.Method.Has<Initializer>())
        {
            // parameter belongs to an initializer, add service to all actions
            foreach (var otherAction in context.Type.Methods.Having<ApiAction>().Select(m => m.Get<ApiAction>()))
            {
                locatorServiceParameter = locatable.AddLocatorAsService(otherAction, context.Parameter.ParameterType);
                if (locatable.IsAsync)
                {
                    otherAction.MakeAsync();
                }
            }
        }

        if (locatorServiceParameter is null) { return; }

        parameter.ConvertToId(idInfo, nullable: !context.Parameter.Has<NotNullAttribute>());
        parameter.LookupRenderer = p => locatable.BuildLocate(locatorServiceParameter, p,
            notNullParameterExpression: $"({idInfo.Type}){p}",
            nullable: !context.Parameter.Has<NotNullAttribute>()
        );
    }
}