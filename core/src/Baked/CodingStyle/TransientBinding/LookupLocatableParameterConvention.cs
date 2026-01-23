using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using System.Diagnostics.CodeAnalysis;

namespace Baked.CodingStyle.TransientBinding;

public class LookupLocatableParameterConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Parameter.ParameterType.TryGetMembers(out var parameterTypeMembers)) { return; }
        if (!context.Parameter.TryGet<ParameterModelAttribute>(out var parameter)) { return; }
        if (!parameterTypeMembers.TryGetIdInfo(out var idInfo)) { return; }
        if (!parameterTypeMembers.TryGet<LocatableAttribute>(out var locatable)) { return; }

        ParameterModelAttribute? locatorServiceParameter = null;
        if (context.Method.TryGet<ActionModelAttribute>(out var action))
        {
            // parameter belongs to an action, add service to the parent action
            locatorServiceParameter = locatable.AddLocatorService(action);
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
                locatorServiceParameter = locatable.AddLocatorService(otherAction);
                if (locatable.IsAsync)
                {
                    otherAction.MakeAsync();
                }
            }
        }

        if (locatorServiceParameter is null) { return; }

        Console.WriteLine(context.Parameter.ParameterType.Name);
        var notNull = context.Parameter.Has<NotNullAttribute>();
        parameter.ConvertToId(idInfo, nullable: !notNull);
        parameter.LookupRenderer = p =>
        {
            return locatable.LookupParameterTemplate(locatorServiceParameter, p, notNull);
        };
    }
}
