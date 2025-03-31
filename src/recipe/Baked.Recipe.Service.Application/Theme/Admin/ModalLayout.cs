using Baked.Ui;

namespace Baked.Theme.Admin;

public record ModalLayout(string Name)
    : INamedComponentSchema
{
    public string Name { get; set; } = Name;
}