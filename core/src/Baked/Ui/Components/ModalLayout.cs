namespace Baked.Ui.Component;

public record ModalLayout(string Path)
    : IGeneratedComponentSchema
{
    public string Path { get; set; } = Path;
}