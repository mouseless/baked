namespace Baked.Playground.CodingStyle.RichTransient;

public class RichTransientParent
{
    public Baked.Business.Id Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;

    internal RichTransientParent With(Baked.Business.Id id)
    {
        Id = id;
        Name = $"{id} parent";
        Description = $"{id} parent description";

        return this;
    }
}