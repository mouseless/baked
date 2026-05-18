using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Database;

public class AddFlatTransactionToActionConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (context.Method.Has<NoTransactionAttribute>()) { return; }

        action.AdditionalAttributes.Add("Baked.Database.FlatTransactionActionFilterAttribute");
    }
}