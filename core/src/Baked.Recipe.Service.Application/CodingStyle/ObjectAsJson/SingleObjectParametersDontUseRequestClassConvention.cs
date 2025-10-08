using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.ObjectAsJson;

public class SingleObjectParametersDontUseRequestClassConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (action.BodyParameters.Count() != 1) { return; }

        var bodyParameter = action.BodyParameters.Single();
        if (bodyParameter.Orphan) { return; }
        if (!context.Method.DefaultOverload.Parameters[bodyParameter.Id].ParameterType.Is<object>()) { return; }

        action.UseRequestClassForBody = false;
    }
}