using Baked.Business;
using Baked.Domain.Model;
using Baked.Ui;
using Humanizer;

using B = Baked.Ui.Components;

namespace Baked.Theme.Default;

public static class DomainComponents
{
    public static ComponentDescriptor<Button> MethodButton(MethodModel method, ComponentContext context,
        Action<Button>? options = default
    )
    {
        context = context.Drill(nameof(Button));
        var (_, l) = context;

        return B.Button(l(method.Name.Humanize().Titleize()),
            action: method.GenerateSchema<RemoteAction>(context.Drill(nameof(IComponentDescriptor.Action))),
            options: options
        );
    }

    public static ComponentDescriptor<SimpleForm> MethodSimpleForm(MethodModel method, ComponentContext context,
        Action<SimpleForm>? options = default
    )
    {
        context = context.Drill(nameof(SimpleForm));
        var (_, l) = context;

        var submit = method.GenerateRequiredComponent<Button>(context.Drill(nameof(SimpleForm.Submit))).Schema;

        return B.SimpleForm(l(method.Name.Titleize()), submit,
            action: method.GenerateSchema<RemoteAction>(context.Drill(nameof(IComponentDescriptor.Action))),
            options: sf =>
            {
                sf.DialogOptions = method.GenerateSchema<SimpleForm.Dialog>(context.Drill(nameof(SimpleForm.DialogOptions)));

                options.Apply(sf);
            }
        );
    }

    public static SimpleForm.Dialog MethodSimpleFormDialog(MethodModel method, ComponentContext context,
        Action<SimpleForm.Dialog>? options = default
    )
    {
        var cancel = method.GenerateRequiredComponent<Button>(context.Drill(nameof(SimpleForm.DialogOptions.Cancel))).Schema;
        var open = method.GenerateRequiredComponent<Button>(context.Drill(nameof(SimpleForm.DialogOptions.Open))).Schema;

        return B.SimpleFormDialog(open, cancel, options: options);
    }

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

    public static ComponentDescriptor<Button> LocalizedButton(string label, ComponentContext context,
        Action<Button>? options = default,
        IAction? action = default
    )
    {
        var (_, l) = context;

        return B.Button(l(label),
            options: options,
            action: action
        );
    }
}