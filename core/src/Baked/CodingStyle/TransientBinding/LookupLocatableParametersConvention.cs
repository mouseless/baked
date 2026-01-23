using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Lifetime;
using Baked.RestApi.Model;
using System.Diagnostics.CodeAnalysis;

namespace Baked.CodingStyle.TransientBinding;

public class LookupLocatableParametersConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Parameter.TryGet<ParameterModelAttribute>(out var parameter)) { return; }
        if (!context.Parameter.ParameterType.Is<IEnumerable>()) { return; }
        if (!context.Parameter.ParameterType.TryGetMembers(out var parameterMembers)) { return; }
        if (!parameterMembers.Has<TransientAttribute>()) { return; }
        if (!parameterMembers.TryGetIdInfo(out var idInfo)) { return; }
        if (!parameterMembers.GetMembers().TryGet<LocatableAttribute>(out var locatable)) { return; }

        var notNull = context.Parameter.Has<NotNullAttribute>();

        ParameterModelAttribute? locatorServiceParameter = null;
        if (context.Method.TryGet<ActionModelAttribute>(out var action))
        {
            // parameter belongs to an action, add service to the parent action
            locatorServiceParameter = locatable.AddLocatorService(action);
        }
        else if (context.Method.Has<InitializerAttribute>())
        {
            // parameter belongs to an initializer, add service to all actions
            foreach (var otherAction in context.Type.Methods.Having<ActionModelAttribute>().Select(m => m.Get<ActionModelAttribute>()))
            {
                locatorServiceParameter = locatable.AddLocatorService(otherAction);
            }
        }

        if (locatorServiceParameter is null) { return; }

        parameter.ConvertToIds(idInfo);
        parameter.LookupRenderer = p => locatable.LookupListParameterTemplate(locatorServiceParameter, p, context.Parameter.ParameterType.IsArray);
    }
}