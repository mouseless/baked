using Baked.Business;

namespace Baked.Playground.Orm;

public class LocatableLabel
{
    public Id Label { get; set; } = default!;
    public string Name => Label;

    public LocatableLabel With(Id label)
    {
        Label = label;

        return this;
    }
}