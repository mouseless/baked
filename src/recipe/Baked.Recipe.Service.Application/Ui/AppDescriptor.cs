namespace Baked.Ui;

public record AppDescriptor
{
    public List<IPlugin> Plugins { get; } = [];
    public IComponentDescriptor? Error { get; set; }
}