using Baked.Domain.Model;
using Baked.Test.Ui;
using Baked.Theme;
using Baked.Ui;

using B = Baked.Ui.Components;
using C = Baked.Test.Ui.Components;

namespace Baked.Test.Theme.Custom;

public static class DomainComponents
{
    public static ComponentDescriptor<Text> MethodText(MethodModel method, ComponentContext context,
        Action<Text>? options = default
    ) => B.Text(
        data: method.GetSchema<RemoteData>(context.Drill(nameof(IComponentDescriptor.Data))),
        options: options
    );

    // TODO - review this in form components
    public static ComponentDescriptor<ContainerPage> TypeContainerPage(
#pragma warning disable IDE0060
        TypeModelMetadata type,
#pragma warning restore IDE0060
        ComponentContext context,
        Action<ContainerPage>? options = default
    )
    {
        context = context.Drill(nameof(ContainerPage));
        var (_, l) = context;

        var path = context.Route.Path.Trim('/');

        return C.ContainerPage(path, options);
    }
}