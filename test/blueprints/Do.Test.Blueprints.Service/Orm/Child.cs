using Do.Orm;

namespace Do.Test.Orm;

public class Child(IEntityContext<Child> _context)
{
    protected Child() : this(default!) { }

    public virtual Guid Id { get; protected set; } = default!;
    public virtual Parent Parent { get; protected set; } = default!;

    protected internal virtual Child With(Parent parent)
    {
        Parent = parent;

        return _context.Insert(this);
    }

    protected internal virtual void Delete() =>
        _context.Delete(this);
}

public class Children(IQueryContext<Child> _context)
{
    internal List<Child> ByParent(Parent parent) =>
        _context.By(e => e.Parent == parent);
}