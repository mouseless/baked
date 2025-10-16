using Baked.Domain.Model;
using Baked.Theme;
using Baked.Ui;

using B = Baked.Ui.Components;

namespace Baked.Test.Theme.Custom;

public static class DomainComponents
{
    public static ComponentDescriptor<Baked.Ui.String> MethodString(MethodModel method, ComponentContext context,
        Action<Baked.Ui.String>? options = default
    ) => B.String(
        data: method.GetSchema<RemoteData>(context.Drill(nameof(IComponentDescriptor.Data))),
        options: options
    );
}