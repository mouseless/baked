using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Binding.Rest;

public class AddMappedMethodAttributeConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModelAttribute>(out var action)) { return; }

        action.AdditionalAttributes.Add($"{typeof(MappedMethodAttribute).FullName}(\"{context.Type.FullName}\", \"{context.Method.Name}\")");
    }
}