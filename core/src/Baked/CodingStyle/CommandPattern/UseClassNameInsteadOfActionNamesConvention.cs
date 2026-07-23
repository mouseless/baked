using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.CommandPattern;

public class UseClassNameInsteadOfActionNamesConvention(
    Func<MethodModelContext, bool>? _whenContext = default
) : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (_whenContext is not null && !_whenContext(context)) { return; }
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }

        action.RouteParts.RemoveAll(action.Name);
        action.Name = context.Type.Name;
    }
}