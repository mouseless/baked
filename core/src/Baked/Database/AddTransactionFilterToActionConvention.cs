using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Database;

public class AddTransactionFilterToActionConvention : IDomainModelConvention<TypeModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMembers(out var members)) { return; }
        if (!members.TryGet<ControllerModelAttribute>(out var controller)) { return; }

        foreach (var (key, action) in controller.Action)
        {
            if (members.Methods.TryGetValue(key, out var method) && method.Has<NoTransactionAttribute>()) { continue; }

            action.AdditionalAttributes.Add($"ServiceFilter(typeof({typeof(TransactionFilter).FullName}), Order = int.MinValue)");
        }
    }
}