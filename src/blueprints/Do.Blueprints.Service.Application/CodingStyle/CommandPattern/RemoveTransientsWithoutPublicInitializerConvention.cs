using Do.Business;
using Do.Lifetime;
using Do.RestApi.Configuration;

namespace Do.CodingStyle.CommandPattern;

public class RemoveTransientsWithoutPublicInitializerConvention : IApiModelConvention<ControllerModelContext>
{
    public void Apply(ControllerModelContext context)
    {
        if (!context.Controller.TypeModel.TryGetMembers(out var members)) { return; }
        if (!members.Has<TransientAttribute>()) { return; }
        if (members.Has<LocatableAttribute>()) { return; }

        var initializer = members.Methods.Having<InitializerAttribute>().Single();
        if (initializer.Overloads.Any(o => o.IsPublic)) { return; } // TODO migrate to metadata check

        context.Api.Controller.Remove(context.Controller.Id);
    }
}