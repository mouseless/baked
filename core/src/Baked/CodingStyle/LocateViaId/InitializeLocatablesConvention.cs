using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.LocateViaId;

public class InitializeLocatablesConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ApiAction>(out var action)) { return; }

        action.AdditionalAttributes.Add($"ServiceFilter(typeof({typeof(InitializeLocatablesFilter).FullName}))");
    }
}