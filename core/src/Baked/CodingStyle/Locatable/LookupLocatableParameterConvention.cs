using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.Locatable;

public class LookupLocatableParameterConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Parameter.ParameterType.TryGetMembers(out var parameterTypeMembers)) { return; }
        if (!context.Parameter.TryGet<ParameterModelAttribute>(out var parameter)) { return; }
        if (!parameterTypeMembers.TryGetIdInfo(out var idInfo)) { return; }
        if (!parameterTypeMembers.TryGet<LocatableAttribute>(out var locatable)) { return; }

        // NOTE action seems to have parameters set from somewhere even though the
        // default overload has zero parameters
        if (!context.Method.DefaultOverload.Parameters.Any()) { return; }

        ParameterModelAttribute? locatorServiceParameter = null;
        if (context.Method.TryGet<ActionModelAttribute>(out var action))
        {
            // parameter belongs to an action, add service to the parent action
            locatorServiceParameter = locatable.AddLocatorAsService(action, context.Parameter.ParameterType);
            if (locatable.IsAsync)
            {
                action.MakeAsync();
            }
        }
        else if (context.Method.Has<InitializerAttribute>())
        {
            // parameter belongs to an initializer, add service to all actions
            foreach (var otherAction in context.Type.Methods.Having<ActionModelAttribute>().Select(m => m.Get<ActionModelAttribute>()))
            {
                locatorServiceParameter = locatable.AddLocatorAsService(otherAction, context.Parameter.ParameterType);
                if (locatable.IsAsync)
                {
                    otherAction.MakeAsync();
                }
            }
        }

        if (locatorServiceParameter is null) { return; }

        parameter.ConvertToId(idInfo);
        parameter.LookupRenderer = p => locatable.LocateRenderer(locatorServiceParameter.Name, p);
    }
}