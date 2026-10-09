using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Lifetime;
using Baked.RestApi.Model;
using System.Diagnostics.CodeAnalysis;

namespace Baked.CodingStyle.Locatable;

public class LookupLocatableParametersConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Parameter.TryGet<ApiParameter>(out var parameter)) { return; }
        if (!context.Parameter.ParameterType.IsAssignableTo<IEnumerable>()) { return; }
        if (!context.Parameter.ParameterType.TryGetElementType(out var elementType)) { return; }
        if (!elementType.TryGetMembers(out var elementMembers)) { return; }
        if (!elementMembers.Has<TransientAttribute>()) { return; }
        if (!elementMembers.TryGetIdInfo(out var idInfo)) { return; }
        if (!elementMembers.GetMembers().TryGet<LocatableAttribute>(out var locatable)) { return; }

        var notNull = context.Parameter.Has<NotNullAttribute>();

        ApiParameter? locatorServiceParameter = null;
        if (context.Method.TryGet<ApiAction>(out var action))
        {
            if (parameter.FromBodyOrForm && !action.UseForm) { return; }

            // parameter belongs to an action, add service to the parent action
            locatorServiceParameter = locatable.AddLocatorAsService(action, elementType);
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
                locatorServiceParameter = locatable.AddLocatorAsService(otherAction, elementType);
                if (locatable.IsAsync)
                {
                    otherAction.MakeAsync();
                }
            }
        }

        if (locatorServiceParameter is null) { return; }

        parameter.ConvertToIds(idInfo);
        parameter.LookupRenderer = p => locatable.BuildLocateMany(locatorServiceParameter, p,
            isArray: context.Parameter.ParameterType.IsArray
        );
    }
}