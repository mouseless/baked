using Baked.Business;

namespace Baked.Playground.Orm;

public class LocatableLabel
{
    public Id Id { get; set; } = default!;
    public string Label => Id;

    public LocatableLabel With(Id id)
    {
        Id = id;

        return this;
    }
}