namespace Baked.Ui.Configuration;

public class LayoutDescriptors : List<IComponentDescriptor>
{
    public void AddLayout<TLayoutSchema>(TLayoutSchema layoutSchema) where TLayoutSchema : IGeneratedComponentSchema =>
        Add(layoutSchema.ToDescriptor());
}