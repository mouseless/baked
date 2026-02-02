using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Lifetime;
using Baked.RestApi.Model;
using Humanizer;
using System.Diagnostics.CodeAnalysis;

namespace Baked.CodingStyle.TransientBinding;

public class LookupLocatableParametersConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Parameter.TryGet<ParameterModelAttribute>(out var parameter)) { return; }
        if (!context.Parameter.ParameterType.IsAssignableTo<IEnumerable>()) { return; }
        if (!context.Parameter.ParameterType.TryGetElementType(out var elementType)) { return; }
        if (!elementType.TryGetMembers(out var elementMembers)) { return; }
        if (!elementMembers.Has<TransientAttribute>()) { return; }
        if (!elementMembers.TryGetIdInfo(out var idInfo)) { return; }
        if (!elementMembers.GetMembers().TryGet<LocatableAttribute>(out var locatable)) { return; }

        var notNull = context.Parameter.Has<NotNullAttribute>();

        ParameterModelAttribute? locatorServiceParameter = null;
        if (context.Method.TryGet<ActionModelAttribute>(out var action))
        {
            // parameter belongs to an action, add service to the parent action
            locatorServiceParameter = locatable.AddAsService(action, elementType.Name.Camelize() + "Locator");
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
                locatorServiceParameter = locatable.AddAsService(otherAction, elementType.Name.Camelize() + "Locator");
                if (locatable.IsAsync)
                {
                    otherAction.MakeAsync();
                }
            }
        }

        if (locatorServiceParameter is null) { return; }

        parameter.ConvertToIds(idInfo);
        parameter.LookupRenderer = p => locatable.LookupManyTemplate(locatorServiceParameter, p,
            isArray: context.Parameter.ParameterType.IsArray,
            castTo: locatable.CastTo
        );
    }
}