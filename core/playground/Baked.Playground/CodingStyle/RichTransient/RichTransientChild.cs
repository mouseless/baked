namespace Baked.Playground.CodingStyle.RichTransient;

public class RichTransientChild(Func<RichTransientParent> _newRichTransientParent)
{
    public Baked.Business.Id Id { get; private set; } = default!;
    public RichTransientParent Parent { get; private set; } = default!;
    public RichTransientParentWrapper ParentWrapper => new(Parent);

    public RichTransientChild With(Baked.Business.Id id)
    {
        Id = id;
        Parent = _newRichTransientParent().With(id);

        return this;
    }
}