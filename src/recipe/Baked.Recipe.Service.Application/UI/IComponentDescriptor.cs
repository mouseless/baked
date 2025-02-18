namespace Baked.UI;

public interface IComponentDescriptor
{
    string Type { get; }
    IComponentSchema? Schema { get; }
    IData? Data { get; }
}