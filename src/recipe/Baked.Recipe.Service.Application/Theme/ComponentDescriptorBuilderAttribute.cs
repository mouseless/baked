using Baked.Ui;

namespace Baked.Theme;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class ComponentDescriptorBuilderAttribute<TSchema> : DescriptorBuilderAttribute<ComponentDescriptor<TSchema>>, IComponentContextBasedBuilder<IComponentDescriptor>
    where TSchema : IComponentSchema
{
    IComponentDescriptor IComponentContextBasedBuilder<IComponentDescriptor>.Build(ComponentContext context) =>
        Build(context);
}