namespace Baked.Playground.CodingStyle.Locatable;

public class ImplementedLocatable : ILocatable
{
    public Baked.Business.Id Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    public ImplementedLocatable With(Baked.Business.Id id)
    {
        Id = id;
        Name = $"{id} name";

        return this;
    }
}