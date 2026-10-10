namespace Baked.Playground.CodingStyle.ResourceViaIdInitializer;

public class ResourceParent
{
    public Baked.Business.Id Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;

    internal ResourceParent With(Baked.Business.Id id)
    {
        Id = id;
        Name = $"{id} parent";
        Description = $"{id} parent description";

        return this;
    }
}