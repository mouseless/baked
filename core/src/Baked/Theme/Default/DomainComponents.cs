using Baked.Business;
using Baked.Domain.Model;
using Baked.Ui;
using Humanizer;

using B = Baked.Ui.Components;

namespace Baked.Theme.Default;

public static class DomainComponents
{
    public static ComponentDescriptor<Fieldset> TypeFieldset(TypeModelMembers type, ComponentContext context,
        Action<Fieldset>? options = default
    ) => TypeFieldset<RemoteData>(type, context, options: options);

    public static ComponentDescriptor<Fieldset> TypeFieldset<TData>(TypeModelMembers type, ComponentContext context,
        Action<Fieldset>? options = default
    ) where TData : IData
    {
        context = context.Drill(nameof(Fieldset));

        var label = type.FirstProperty<LabelAttribute>();
        if (!label.TryGet<DataAttribute>(out var labelData))
        {
            throw DiagnosticCode.PropertyWithAttribute.Exception(
                $"`{label.Name}` should have a `{nameof(DataAttribute)}`"
            );
        }

        var data = type.GenerateSchema<TData>(context.Drill(nameof(IComponentDescriptor.Data)));

        return B.Fieldset(labelData.Prop, options: options, data: data);
    }

    public static Field PropertyField(PropertyModel property, ComponentContext context,
        Action<Field>? options = default
    )
    {
        context = context.Drill(property.Name);
        var (_, l) = context;

        return B.Field(property.Name.Camelize(), l(property.Name.Titleize()),
            options: f =>
            {
                f.Component = property.GenerateComponent(context.Drill(nameof(Field.Component))) ?? f.Component;

                options.Apply(f);
            }
        );
    }

    public static ComponentDescriptor<Dialog> PropertyDialog(PropertyModel property, ComponentContext context,
        Action<Dialog>? options = default
    )
    {
        context = context.Drill(nameof(Dialog));
        var (_, l) = context;

        var open = property.GenerateRequiredComponent<Button>(context.Drill(nameof(Dialog.Open)));
        var content = property.GenerateRequiredComponent(context.Drill(nameof(Dialog.Content)));

        return B.Dialog(
            open: open.Schema,
            header: l(property.Name.Titleize()),
            content: content,
            options: options
        );
    }

    public static ComponentDescriptor<NavLink> TypeNavLink(TypeModelMetadata type, ComponentContext context,
        Action<NavLink>? options = default
    )
    {
        context = context.Drill(nameof(NavLink));

        if (!type.TryGet<RouteAttribute>(out var pageAttribute))
        {
            throw DiagnosticCode.TypeWithAttribute.Exception(
                $"`{nameof(RouteAttribute)}` is not found on type (`{type.Name}`) to render as `{nameof(NavLink)}`"
            );
        }

        return B.NavLink(pageAttribute.Path, options: options);
    }
}