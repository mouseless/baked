using Baked.Ui;

namespace Baked.Theme.Admin;

public record ModalLayout(string Path)
    : IGeneratedComponentSchema
{
    public string Path { get; set; } = Path;
}