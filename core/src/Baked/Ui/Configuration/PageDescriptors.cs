namespace Baked.Ui.Configuration;

public class PageDescriptors : List<IComponentDescriptor>
{
    public void AddPage<TPageSchema>(TPageSchema pageSchema) where TPageSchema : IPageSchema =>
        Add(pageSchema.ToDescriptor());
}