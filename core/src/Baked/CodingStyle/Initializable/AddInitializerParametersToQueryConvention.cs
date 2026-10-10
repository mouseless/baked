using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Lifetime;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.Initializable;

public class AddInitializerParametersToQueryConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Type.Has<Transient>()) { return; }
        if (!context.Type.TryGetMembers(out var members)) { return; }
        if (members.Has<LocatableAttribute>()) { return; }
        if (!members.Methods.Having<Initializer>().Any()) { return; }
        if (!context.Method.TryGet<ApiAction>(out var action)) { return; }

        var initializer = members.Methods.Having<Initializer>().Single();
        foreach (var parameter in initializer.DefaultOverload.Parameters)
        {
            if (!parameter.TryGet<ApiParameter>(out var apiParameter)) { continue; }

            apiParameter.From = ParameterModelFrom.Query;
            apiParameter.IsInvokeMethodParameter = false;

            action.Parameter[parameter.Name] = apiParameter;
        }
    }
}