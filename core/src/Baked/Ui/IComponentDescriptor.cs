namespace Baked.Ui;

public interface IComponentDescriptor
{
    string Type { get; }
    IComponentSchema Schema { get; }
    IData? Data { get; set; }

    string? Binding { get; set; }
}