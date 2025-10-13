using Baked.Ui;

namespace Baked.Theme.Default;

public record String : IComponentSchema
{
    public int? MaxLength { get; set; }
}