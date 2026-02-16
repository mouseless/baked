using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.Locatable;

public class InitializeLocatablesConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }

        action.AdditionalAttributes.Add($"ServiceFilter(typeof({typeof(InitializeLocatablesFilter).FullName}))");
    }
}