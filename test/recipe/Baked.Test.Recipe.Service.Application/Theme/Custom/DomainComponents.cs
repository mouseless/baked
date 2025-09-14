using Baked.Domain.Model;
using Baked.Theme;
using Baked.Ui;

using static Baked.Theme.Admin.Components;

namespace Baked.Test.Theme.Custom;

public static class DomainComponents
{
    public static ComponentDescriptor<Baked.Theme.Admin.String> MethodString(MethodModel method, ComponentContext context,
        Action<Baked.Theme.Admin.String>? options = default
    ) => String(
        data: method.GetSchema<RemoteData>(context.Drill(nameof(IComponentDescriptor.Data))),
        options: options
    );
}