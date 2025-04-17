using Baked.Ui;

namespace Baked.Theme.Admin;

public record String : IComponentSchema
{
    public int? MaxLength { get; set; }
}