using Baked.Orm;

namespace Baked.Playground.Orm;

public class Child(IEntityContext<Child> _context)
{
    public Guid Id { get; private set; } = default!;
    public Parent Parent { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    internal Parent? InternalParent { get; private set; } = default!;

    internal Child With(Parent parent, string name)
    {
        Parent = parent;
        Name = name;

        return _context.Insert(this);
    }

    internal void Delete() =>
        _context.Delete(this);
}

public class Children(IQueryContext<Child> _context)
{
    internal List<Child> ByParent(Parent parent) =>
        _context.By(e => e.Parent == parent);

    public List<Child> By() =>
        _context.By(c => true);
}