namespace Baked.Ui;

public interface INamedComponentSchema : IComponentSchema
{
    string Name { get; }
    public string Route => string.Empty;
}