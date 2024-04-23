using Do.Business;
using Do.Lifetime;
using Do.RestApi.Configuration;

namespace Do.CodingStyle.CommandPattern;

public class RemoveTransientServicesWithNonPublicInitializerConvention : IApiModelConvention<ControllerModelContext>
{
    public void Apply(ControllerModelContext context)
    {
        if (!context.Controller.TypeModel.TryGetMembers(out var members)) { return; }
        if (!members.Has<TransientAttribute>()) { return; }

        var initializers = members.Methods.Having<InitializerAttribute>();
        if (initializers.Any(i => i.Overloads.Any(o => o.IsPublic && o.AllParametersAreApiInput()))) { return; }

        context.Api.Controller.Remove(context.Controller.Id);
    }
}