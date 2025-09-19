using Baked.Domain.Model;
using Baked.Theme;
using Baked.Ui;

using static Baked.Theme.Default.Components;

namespace Baked.Test.Theme.Custom;

public static class DomainComponents
{
    public static ComponentDescriptor<Baked.Theme.Default.String> MethodString(MethodModel method, ComponentContext context,
        Action<Baked.Theme.Default.String>? options = default
    ) => String(
        data: method.GetSchema<RemoteData>(context.Drill(nameof(IComponentDescriptor.Data))),
        options: options
    );
}