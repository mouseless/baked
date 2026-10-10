using Baked.Ui;

namespace Baked.Theme;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class ComponentGenerator<TSchema> : GeneratorAttribute<ComponentDescriptor<TSchema>>, IComponentContextBasedGenerator<IComponentDescriptor>
    where TSchema : IComponentSchema
{
    IComponentDescriptor IComponentContextBasedGenerator<IComponentDescriptor>.Generate(ComponentContext context) =>
        Generate(context);
}