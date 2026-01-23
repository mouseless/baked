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

        locatable.AddLocatorService = (action) => action.AddFactoryAsService(context.Type);
        locatable.FindTargetTemplate = (locatorServiceParameter, p) => context.Type.BuildInitializerById(p.Name);
        locatable.LookupParameterTemplate = (locatorServiceParameter, p, notnull) => context.Type.BuildInitializerById(p,
            nullable: !notnull
        );
        locatable.LookupListParameterTemplate = (locatorServiceParameter, p, isArray) => context.Type.BuildInitializerByIds(p,
           isArray: isArray
       );
    }
}