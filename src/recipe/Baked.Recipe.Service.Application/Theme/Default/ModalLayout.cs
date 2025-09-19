using Baked.Ui;

namespace Baked.Theme.Default;

public record ModalLayout(string Path)
    : IGeneratedComponentSchema
{
    public string Path { get; set; } = Path;
}