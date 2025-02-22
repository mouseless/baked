using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Binding.Rest;

public class AddTargetParameterConvention(string parameterName)
    : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModelAttribute>(out var action)) { return; }

        action.Parameter[parameterName] = new ParameterModelAttribute(parameterName, context.Type.CSharpFriendlyFullName, ParameterModelFrom.Services);
    }
}