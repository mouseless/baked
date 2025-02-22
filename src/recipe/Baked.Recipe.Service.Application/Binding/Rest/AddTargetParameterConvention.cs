using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Binding.Rest;

public class AddTargetParameterConvention(string parameterName)
    : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModel>(out var action)) { return; }

        action.Parameter[parameterName] = new ParameterModel(parameterName, context.Type.CSharpFriendlyFullName, ParameterModelFrom.Services);
    }
}