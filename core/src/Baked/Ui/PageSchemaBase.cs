namespace Baked.Ui;

public abstract record PageSchemaBase : IPageSchema
{
    string _path = string.Empty;

    public string Path
    {
        get => _path;
        set => _path = value.Trim('/');
    }

    public string? Layout { get; set; }
}