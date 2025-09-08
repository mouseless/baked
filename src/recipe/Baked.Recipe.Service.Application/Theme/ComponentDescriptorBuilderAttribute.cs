using Baked.Ui;

namespace Baked.Theme;

public class ComponentDescriptorBuilderAttribute<TSchema>(
    Func<ComponentContext, ComponentDescriptorAttribute<TSchema>>? builder = default,
    Func<ComponentContext, bool>? when = default
) : Attribute, IComponentDescriptorBuilder
    where TSchema : IComponentSchema
{
    readonly Func<ComponentContext, bool> _when = when ?? (cc => true);

    public Func<ComponentContext, ComponentDescriptorAttribute<TSchema>>? Builder { get; set; } = builder;

    public bool AppliesTo(ComponentContext context) =>
        _when(context);

    public ComponentDescriptorAttribute<TSchema> Build(ComponentContext context)
    {
        if (Builder is null) { throw new ArgumentNullException(nameof(Builder), $"`Builder` was not set for this attribute"); }

        return Builder(context);
    }

    IComponentDescriptor IComponentDescriptorBuilder.Build(ComponentContext context) =>
        Build(context);
}