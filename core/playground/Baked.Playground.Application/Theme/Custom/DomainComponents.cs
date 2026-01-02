using Baked.Domain.Model;
using Baked.Theme;
using Baked.Ui;

using B = Baked.Ui.Components;

namespace Baked.Playground.Theme.Custom;

public static class DomainComponents
{
    public static ComponentDescriptor<Text> MethodText(MethodModel method, ComponentContext context,
        Action<Text>? options = default
    ) => B.Text(
        data: method.GetSchema<RemoteData>(context.Drill(nameof(IComponentDescriptor.Data))),
        options: options
    );
}