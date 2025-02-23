using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Binding.Rest;

public class AddTargetParameterConvention()
    : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModelAttribute>(out var action)) { return; }

        action.Parameter[ParameterModelAttribute.TargetParameterName] = new ParameterModelAttribute(ParameterModelAttribute.TargetParameterName, context.Type.CSharpFriendlyFullName, ParameterModelFrom.Services);
    }
}