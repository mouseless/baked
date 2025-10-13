namespace Baked.Ui;

public interface IGeneratedComponentSchema : IComponentSchema
{
    string Path { get; }

    public string Name => Path.Split("/")[^1];
    public string Dir => Path.Split("/")[..^1].Join("/");
}