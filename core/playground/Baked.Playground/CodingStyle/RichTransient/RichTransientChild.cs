using Baked.Playground.CodingStyle.Locatable;

namespace Baked.Playground.CodingStyle.RichTransient;

public class RichTransientChild(Func<RichTransientParent> _newRichTransientParent, Func<ImplementedLocatable> _newImplementedLocatable)
{
    public Baked.Business.Id Id { get; private set; } = default!;
    public RichTransientParent Parent { get; private set; } = default!;
    public RichTransientParentWrapper ParentWrapper => new(Parent);
    public ILocatable Interface => _newImplementedLocatable().With(Id);

    public RichTransientChild With(Baked.Business.Id id)
    {
        Id = id;
        Parent = _newRichTransientParent().With(id);

        return this;
    }
}