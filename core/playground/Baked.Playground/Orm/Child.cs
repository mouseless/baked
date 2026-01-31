using Baked.Business;
using Baked.Orm;

namespace Baked.Playground.Orm;

public class Child(IEntityContext<Child> _context)
{
    public Id Id { get; private set; } = default!;
    public Parent Parent { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    internal Parent? InternalParent { get; private set; } = default!;
    public ParentWrapper ParentWrapper => new(Parent);

    internal Child With(Parent parent, string name)
    {
        Parent = parent;
        Name = name;

        return _context.Insert(this);
    }

    internal void Delete() =>
        _context.Delete(this);

    public void Update(
        string? name = default,
        Parent? parent = default,
        ParentWrapper? parentWrapper = default
      )
    {
        Name = name ?? Name;
        Parent = parent ?? parentWrapper?.Parent ?? Parent;
    }
}

public class Children(IQueryContext<Child> _context)
{
    internal List<Child> ByParent(Parent parent) =>
        _context.By(e => e.Parent == parent);

    public List<Child> By() =>
        _context.By(c => true);

    public Child? FirstBy(bool fetchParents = true) =>
        _context.Query(fetchParents: fetchParents).FirstOrDefault();
}