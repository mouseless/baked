using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.RestApi.Conventions;

public class ConfigureActionConvention<T>(string name, Action<ApiAction> configure)
    : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Type.Is<T>()) { return; }
        if (!context.Method.TryGet<ApiAction>(out var action)) { return; }
        if (action.Id != name) { return; }

        configure(action);
    }
}