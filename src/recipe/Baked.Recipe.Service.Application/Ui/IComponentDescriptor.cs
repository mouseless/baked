namespace Baked.Ui;

public interface IComponentDescriptor
{
    string Type { get; }
    string? Key { get; }
    IComponentSchema? Schema { get; }
    IData? Data { get; }
}