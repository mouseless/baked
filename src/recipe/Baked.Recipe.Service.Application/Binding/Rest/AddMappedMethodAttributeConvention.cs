using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Binding.Rest;

public class AddMappedMethodAttributeConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModel>(out var action)) { return; }

        action.AdditionalAttributes.Add($"{typeof(MappedMethodAttribute).FullName}(\"{context.Type.FullName}\", \"{context.Method.Name}\")");
    }
}