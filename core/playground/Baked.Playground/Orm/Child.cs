using Baked.Orm;

namespace Baked.Playground.Orm;

public class Child(IEntityContext<Child> _context)
{
    protected Child() : this(default!) { }

    public virtual Guid Id { get; protected set; } = default!;
    public virtual Parent Parent { get; protected set; } = default!;
    public virtual string Name { get; protected set; } = default!;
    protected internal virtual Parent? InternalParent { get; protected set; } = default!;

    protected internal virtual Child With(Parent parent, string name)
    {
        Parent = parent;
        Name = name;

        return _context.Insert(this);
    }

    protected internal virtual void Delete() =>
        _context.Delete(this);
}

public class Children(IQueryContext<Child> _context)
{
    internal List<Child> ByParent(Parent parent) =>
        _context.By(e => e.Parent == parent);

    public List<Child> By() =>
        _context.By(c => true);
}