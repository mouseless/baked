using Baked.Playground.CodingStyle.LocateViaId;

namespace Baked.Playground.CodingStyle.ResourceViaIdInitializer;

public class ResourceChild(Func<ResourceParent> _newResourceParent, Func<ImplementedLocatable> _newImplementedLocatable)
{
    public Baked.Business.Id Id { get; private set; } = default!;
    public ResourceParent Parent { get; private set; } = default!;
    public ResourceParentWrapper ParentWrapper => new(Parent);
    public ILocatable Interface => _newImplementedLocatable().With(Id);

    public ResourceChild With(Baked.Business.Id id)
    {
        Id = id;
        Parent = _newResourceParent().With(id);

        return this;
    }
}