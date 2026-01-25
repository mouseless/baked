using Baked.Business;
using Baked.Domain.Configuration;

namespace Baked.CodingStyle.RichTransient;

public class LocateUsingInitializerConvention : IDomainModelConvention<TypeModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMembers(out var members)) { return; }
        if (!members.TryGet<LocatableAttribute>(out var locatable)) { return; }

        var initializer = members.Methods.Having<InitializerAttribute>().Single();
        if (!initializer.DefaultOverload.Parameters.TryGetValue("id", out var idParameter)) { return; }

        var actionShouldBeAsync = initializer.DefaultOverload.ReturnType.IsAssignableTo<Task>();

        locatable.IsAsync = actionShouldBeAsync;
        context.Type.Apply(t => locatable.ServiceType = t);
        locatable.LocateSingleMethodName = initializer.Name;
        locatable.IsFactoryService = true;
    }
}